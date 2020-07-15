using FlightSimulator.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

//https://airport.api.aero/airport/distance/DRS/FUE?user_key=d805e84363494ca03b9b52d5a505c4d1"
//https://www.geodatasource.com/developers/c-sharp
namespace FlightSimulator.Services
{
    public interface ISimulationService
    {
        SimulationResult SimulateFlight(Airport fromAirport, Airport toAirport, Aircraft aircraft);
        SimulationResult SimulateFlight(string fromIata, string toIata, string aircraftModel);
    }

    public class SimulationService : ISimulationService
    {
        private readonly IAirportService airportService;
        private readonly IAircraftService aircraftService;

        public SimulationService(IAirportService airportService, IAircraftService aircraftService)
        {
            this.airportService = airportService;
            this.aircraftService = aircraftService;
        }

        public SimulationResult SimulateFlight(string fromIata, string toIata, string aircraftModel)
        {
            var fromAirport = airportService.GetByIataCode(fromIata);
            var toAirport = airportService.GetByIataCode(toIata);
            var aircraft = aircraftService.GetByModel(aircraftModel);
            return SimulateFlight(fromAirport, toAirport, aircraft);
        }

        public SimulationResult SimulateFlight(Airport fromAirport, Airport toAirport, Aircraft aircraft)
        {
            var distance = Distance(fromAirport.Latitude, fromAirport.Longitude, toAirport.Latitude, toAirport.Longitude, 'K');
            var r = new SimulationResult();
            r.Distance = (int)distance;
            r.FuelConsumption = (int)(aircraft.FuelConsumption * distance / 100);
            r.Timespan = new TimeSpan(0, 0, (int)distance * 3600 / aircraft.Speed);
            return r;
        }

        private double Distance(double lat1, double lon1, double lat2, double lon2, char unit)
        {
            if ((lat1 == lat2) && (lon1 == lon2))
            {
                return 0;
            }
            else
            {
                double theta = lon1 - lon2;
                double dist = Math.Sin(Deg2rad(lat1)) * Math.Sin(Deg2rad(lat2)) + Math.Cos(Deg2rad(lat1)) * Math.Cos(Deg2rad(lat2)) * Math.Cos(Deg2rad(theta));
                dist = Math.Acos(dist);
                dist = Rad2deg(dist);
                dist = dist * 60 * 1.1515;
                if (unit == 'K')
                {
                    dist = dist * 1.609344;
                }
                else if (unit == 'N')
                {
                    dist = dist * 0.8684;
                }
                return (dist);
            }
        }

        private double Deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private double Rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
}

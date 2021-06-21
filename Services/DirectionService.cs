using GoogleApi.Entities.Common;
using GoogleApi.Entities.Maps.Directions.Request;
using GoogleApi.Entities.Maps.Directions.Response;
using GoogleApi.Entities.Maps.StaticMaps.Request;
using System.Threading.Tasks;
using System.Configuration;
using DirectionsApp.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Drawing.Imaging;

namespace DirectionsApp
{
    public static class DirectionService
    {
        public static async Task<Directions> GetDirectionsAsync(Parameters UserParameters)
        {
            var request = new DirectionsRequest();
            request.Key = ConfigurationManager.AppSettings.Get("apikey");
            request.Origin = new Location(UserParameters.Start);
            request.Destination = new Location(UserParameters.End);
            //request.DepartureTime = UserParameters.DepartureTime;

            var googleResponse = await GoogleApi.GoogleMaps.Directions.QueryAsync(request);

            var DirectionsResponse = new Directions();
            DirectionsResponse.Start = UserParameters.Start;
            DirectionsResponse.End = UserParameters.End;
            foreach (var Leg in googleResponse.Routes.First().Legs)
            {
                DirectionsResponse.TotalDistance += Leg.Distance.Value;
                DirectionsResponse.TotalDuration += Leg.Duration.Value;
                DirectionsResponse.Route = new List<string>();
                if (UserParameters.map)
                {
                    var mapPoints = new List<Location>();
                    var mapPaths = new List<MapPath>();
                    foreach (var step in Leg.Steps)
                    {
                        mapPoints.Add(step.StartLocation);
                    }
                    mapPaths.Add(new MapPath() { Points = mapPoints });
                    var staticMapRequest = new StaticMapsRequest()
                    {
                        Paths = mapPaths,
                        Key = request.Key
                    };
                    DirectionsResponse.MapURL = staticMapRequest.GetUri().ToString();
                }
                foreach (var step in Leg.Steps)
                {
                    DirectionsResponse.Route.Add($"\n{step.HtmlInstructions.ToString()}");
                }
            }
            return DirectionsResponse;
        }
    }
}

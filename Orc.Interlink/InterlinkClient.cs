using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;

namespace Orc.Interlink
{
    public class InterlinkClient
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public RestClient Client { get; set; }
        public string GeoSession { get; set; }
        public InterlinkClient(string username, string password)
        {
            Username = username;
            Password = password;
            Client = new RestClient("https://api.interlinkexpress.com");
            //Client.Proxy = new WebProxy(new Uri("http://localhost:8888"));
        }

        public void Login()
        {

            Client.Authenticator = new HttpBasicAuthenticator(Username, Password);
            var req = new RestRequest("/user/?action=login");
            var response = Client.ExecuteAsPost<InterLinkResponse<LoginResponse>>(req, "POST");
            Client.Authenticator = null;
            GeoSession = response.Data.Data.SessionId;
            Client.AddDefaultHeader("GeoClient", "account/2175822");
            Client.AddDefaultHeader("GeoSession", GeoSession);

        }

        public InterLinkResponse<List<DeliveryService>> GetServicesForAddress(int businessUnit, DeliveryDirection direction, int numberOfParcels, double totalWeight, Address deliveryAddress, Address collectionAddress)
        {
            var req = new RestRequest("/shipping/network");
            req.AddQueryParameter("businessUnit", businessUnit.ToString());
            switch (direction)
            {
                case DeliveryDirection.OutboundShipments:
                    req.AddQueryParameter("deliveryDirection", "1");
                    break;
                case DeliveryDirection.InboundShipments:
                    req.AddQueryParameter("deliveryDirection", "2");
                    break;
                default:
                    throw new ArgumentOutOfRangeException("direction", direction, null);
            }


            req.AddQueryParameter("numberOfParcels", numberOfParcels.ToString());

            req.AddQueryParameter("totalWeight", totalWeight.ToString());
            req.AddQueryParameter("deliveryDetails.address.countryCode", deliveryAddress.countryCode);
            req.AddQueryParameter("deliveryDetails.address.countryName", deliveryAddress.countryName);
            req.AddQueryParameter("deliveryDetails.address.locality", deliveryAddress.locality);
            req.AddQueryParameter("deliveryDetails.address.organisation", deliveryAddress.organisation);
            req.AddQueryParameter("deliveryDetails.address.postcode", deliveryAddress.postcode);
            req.AddQueryParameter("deliveryDetails.address.property", deliveryAddress.property);
            req.AddQueryParameter("deliveryDetails.address.street", deliveryAddress.street);
            req.AddQueryParameter("deliveryDetails.address.town", deliveryAddress.town);
            req.AddQueryParameter("deliveryDetails.address.county", deliveryAddress.county);

            req.AddQueryParameter("collectionDetails.address.countryCode", collectionAddress.countryCode);
            req.AddQueryParameter("collectionDetails.address.countryName", collectionAddress.countryName);
            req.AddQueryParameter("collectionDetails.address.locality", collectionAddress.locality);
            req.AddQueryParameter("collectionDetails.address.organisation", collectionAddress.organisation);
            req.AddQueryParameter("collectionDetails.address.postcode", collectionAddress.postcode);
            req.AddQueryParameter("collectionDetails.address.property", collectionAddress.property);
            req.AddQueryParameter("collectionDetails.address.street", collectionAddress.street);
            req.AddQueryParameter("collectionDetails.address.town", collectionAddress.town);
            req.AddQueryParameter("collectionDetails.address.county", collectionAddress.county);

            var resp = Client.Get<InterLinkResponse<List<DeliveryService>>>(req).Data;
            return resp;
        }
        public InterLinkResponse<InsertShipmentResponse> InsertShipment(ShipmentRequest request, bool isTest)
        {
            var req = new RestRequest("/shipping/shipment");
            if (isTest)
            {
                req.AddQueryParameter("test", "true");
            }
            req.AddJsonBody(request);
            var resp = Client.Post < InterLinkResponse<InsertShipmentResponse>>(req);
            var responseModel = resp.Data;
            responseModel.RawResponse = resp.Content;
            responseModel.ServerResponseCode = resp.StatusCode;
            return responseModel;
        }
        public string GetLabel(string shipmentId, LabelFormat format)
        {
            var req = new RestRequest("/shipping/shipment/{ShipmentId}/label");

            req.AddUrlSegment("ShipmentId", shipmentId);
            switch (format)
            {
                case LabelFormat.Citizen:
                    req.AddHeader("Accept", "text/vnd.citizen-clp");
                    break;
                case LabelFormat.Eltron:
                    req.AddHeader("Accept", "text/vnd.eltron-epl");
                    break;
                case LabelFormat.Html:
                    req.AddHeader("Accept", "text/html");
                    break;
                default:
                    throw new ArgumentOutOfRangeException("format", format, null);
            }

            var data = Client.Get(req);
            return data.Content;
        }


    }
}

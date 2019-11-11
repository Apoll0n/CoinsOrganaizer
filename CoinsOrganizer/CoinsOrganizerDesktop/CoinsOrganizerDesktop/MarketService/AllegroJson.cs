using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CoinsOrganizerDesktop.MarketService
{

    [DebuggerDisplay("Publication = {" + nameof(Publication) + "}")]
    public class PublicationOfferJson
    {
        [JsonProperty("publication")] public PublicationJson Publication { get; set; }

        [JsonProperty("offerCriteria")] public OfferCriteriaJson OfferCriteria { get; set; }

        public class OfferCriteriaJson
        {
            [JsonProperty("offers")] public IList<OfferIdJson> Offers { get; set; }

            [JsonProperty("type")] public string Type { get; set; }

            public class OfferIdJson
            {
                [JsonProperty("id")] public long Id { get; set; }
            }
        }

        public class PublicationJson
        {
            [JsonProperty("action")] public string Action { get; set; }

            [JsonProperty("scheduledFor")] public DateTime? ScheduledFor { get; set; }
        }
    }

    public class AuthorizationJson
    {
        [JsonProperty("access_token")] public string AccessToken { get; set; }

        [JsonProperty("token_type")] public string TokenType { get; set; }

        [JsonProperty("expires_in")] public string ExpiresIn { get; set; }

        [JsonProperty("scope")] public string Scope { get; set; }

        [JsonProperty("jti")] public string Jti { get; set; }
    }

    public class OffersJson
    {
        [JsonProperty("offers")] public IList<OfferJson> Offers { get; set; }

    }

    public class OfferJson
    {
        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("category")] public CategoryJson Category { get; set; }

        [JsonProperty("product")] public PrimaryImageJson Product { get; set; }

        [JsonProperty("parameters")] public IList<ParametersJson> Parameters { get; set; }

        [JsonProperty("ean")] public string Ean { get; set; }

        [JsonProperty("description")] public DescriptionJson Description { get; set; }

        [JsonProperty("images")] public IList<ImageUrlJson> Images { get; set; }

        [JsonProperty("primaryImage")] public PrimaryImageJson PrimaryImage { get; set; }

        [JsonProperty("sellingMode")] public SellingModeJson SellingMode { get; set; }

        [JsonProperty("saleInfo")] public SaleInfoJson SaleInfo { get; set; }

        [JsonProperty("stats")] public StatsJson Stats { get; set; }

        [JsonProperty("stock")] public StockJson Stock { get; set; }

        [JsonProperty("publication")] public PublicationJson Publication { get; set; }

        [JsonProperty("delivery")] public DeliveryJson Delivery { get; set; }

        [JsonProperty("payments")] public PaymentsJson Payments { get; set; }

        [JsonProperty("afterSalesServices")] public AfterSalesServicesJson AfterSalesServices { get; set; }

        [JsonProperty("additionalServices")] public string AdditionalServices { get; set; }

        [JsonProperty("sizeTable")] public string SizeTable { get; set; }

        [JsonProperty("promotion")] public PromotionJson Promotion { get; set; }

        [JsonProperty("location")] public LocationJson Location { get; set; }

        [JsonProperty("external")] public ExternalJson External { get; set; }

        [JsonProperty("contact")] public string Contact { get; set; }

        [JsonProperty("validation")] public ValidationJson Validation { get; set; }

        [JsonProperty("createdAt")] public string CreatedAt { get; set; }

        [JsonProperty("updatedAt")] public string UpdatedAt { get; set; }

        public class ValidationJson
        {
            [JsonProperty("errors")] public IList<string> Errors { get; set; }

            [JsonProperty("validatedAt")] public string ValidatedAt { get; set; }
        }

        public class ExternalJson
        {
            [JsonProperty("id")] public string Id { get; set; }
        }

        public class LocationJson
        {
            [JsonProperty("countryCode")] public string CountryCode { get; set; }

            [JsonProperty("province")] public string Province { get; set; }

            [JsonProperty("city")] public string City { get; set; }

            [JsonProperty("postCode")] public string PostCode { get; set; }
        }

        public class PromotionJson
        {
            [JsonProperty("emphasized")] public string Emphasized { get; set; }

            [JsonProperty("bold")] public string Bold { get; set; }

            [JsonProperty("highlight")] public string Highlight { get; set; }

            [JsonProperty("departmentPage")] public string DepartmentPage { get; set; }

            [JsonProperty("emphasizedHighlightBoldPackage")]
            public string EmphasizedHighlightBoldPackage { get; set; }
        }

        public class PaymentsJson
        {
            [JsonProperty("invoice")] public string Invoice { get; set; }
        }

        public class DeliveryJson
        {
            [JsonProperty("shipmentDate")] public string ShipmentDate { get; set; }

            [JsonProperty("additionalInfo")] public string AdditionalInfo { get; set; }

            [JsonProperty("handlingTime")] public string HandlingTime { get; set; }

            [JsonProperty("shippingRates")] public ShippingRatesJson ShippingRates { get; set; }

            public class ShippingRatesJson
            {
                [JsonProperty("id")] public string Id { get; set; }
            }
        }

        public class ImageUrlJson
        {
            [JsonProperty("url")] public string Url { get; set; }
        }

        public class DescriptionJson
        {
            [JsonProperty("sections")] public IList<DescriptionSectionJson> Sections { get; set; }

            public class DescriptionSectionJson
            {
                [JsonProperty("items")] public IList<DescriptionSectionItemsJson> Items { get; set; }

                public class DescriptionSectionItemsJson
                {
                    [JsonProperty("type")] public string Type { get; set; }

                    [JsonProperty("content")] public string Content { get; set; }

                    [JsonProperty("url")] public string Url { get; set; }
                }

            }
        }

        public class ParametersJson
        {
            [JsonProperty("id")] public string Id { get; set; }

            [JsonProperty("valuesIds")] public IList<string> ValuesIds { get; set; }

            [JsonProperty("values")] public IList<string> Values { get; set; }

            [JsonProperty("rangeValue")] public string RangeValue { get; set; }
        }

        public class AfterSalesServicesJson
        {
            [JsonProperty("warranty")] public string Warranty { get; set; }

            [JsonProperty("returnPolicy")] public string ReturnPolicy { get; set; }

            [JsonProperty("impliedWarranty")] public string ImpliedWarranty { get; set; }
        }

        public class PublicationJson
        {
            [JsonProperty("duration")] public string Duration { get; set; }

            [JsonProperty("status")] public string Status { get; set; }

            [JsonProperty("startingAt")] public string StartingAt { get; set; }

            [JsonProperty("startedAt")] public string StartedAt { get; set; }

            [JsonProperty("endingAt")] public string EndingAt { get; set; }

            [JsonProperty("endedAt")] public string EndedAt { get; set; }
        }

        public class StockJson
        {
            [JsonProperty("available")] public string Available { get; set; }

            [JsonProperty("sold")] public string Sold { get; set; }

            [JsonProperty("unit")] public string Unit { get; set; }

        }

        public class StatsJson
        {
            [JsonProperty("watchersCount")] public string WatchersCount { get; set; }

            [JsonProperty("visitsCount")] public string VisitsCount { get; set; }
        }

        public class SaleInfoJson
        {
            [JsonProperty("currentPrice")] public CurrentPriceJson CurrentPrice { get; set; }

            [JsonProperty("biddersCount")] public string BiddersCount { get; set; }

            public class CurrentPriceJson
            {
                [JsonProperty("amount")] public string Amount { get; set; }

                [JsonProperty("currency")] public string Currency { get; set; }
            }
        }

        public class CategoryJson
        {
            [JsonProperty("id")] public string Id { get; set; }
        }

        public class PrimaryImageJson
        {
            [JsonProperty("url")] public string Url { get; set; }
        }

        public class SellingModeJson
        {
            [JsonProperty("format")] public string Format { get; set; }

            [JsonProperty("price")] public string Price { get; set; }

            [JsonProperty("minimalPrice")] public string MinimalPrice { get; set; }

            [JsonProperty("startingPrice")] public StartingPriceJson StartingPrice { get; set; }

            public class StartingPriceJson
            {
                [JsonProperty("amount")] public string Amount { get; set; }

                [JsonProperty("currency")] public string Currency { get; set; }
            }
        }
    }
}

using System.Collections.Generic;
using Newtonsoft.Json;

namespace BlaiseCaseAutoComplete.Models
{
    public class AutoCompleteCaseModel
    {
        [JsonProperty("instrument_name")]
        public string InstrumentName { get; set; }

        [JsonProperty("server_park")]
        public string ServerPark { get; set; }

        [JsonProperty("primary_key")]
        public string PrimaryKey { get; set; }

        public int NumberOfCases { get; set; }

        public Dictionary<string, string> Payload { get; set; }

        public bool IsSpecificCase => !string.IsNullOrWhiteSpace(PrimaryKey);
    }
}

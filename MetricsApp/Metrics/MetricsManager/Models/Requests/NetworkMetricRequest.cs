﻿namespace MetricsManager.Models.Requests
{
    public class NetworkMetricRequest
    {
        public int AgentId { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
    }
}

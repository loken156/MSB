namespace Application.Dto.Detrack
{
    public class DetrackJobDto
    {
        public JobData data { get; set; } // Wrap the job details in a data object
    
        public class JobData
        {
            public string do_number { get; set; }  // Unique delivery order number
            public string type { get; set; } = "Delivery"; // Default to "Delivery"
            public string primary_job_status { get; set; } = "dispatched";
            public string date { get; set; } // Date of the job, format "yyyy-MM-dd"
            public string order_number { get; set; }
            public string address { get; set; } // Full delivery address
            public string postal_code { get; set; }
            public string deliver_to_collect_from { get; set; } // Name of the recipient
            public string phone_number { get; set; } // Recipient's phone number
            public string instructions { get; set; } // Any specific instructions
            public string notify_email { get; set; } = "test@example.com"; // Notification email, can be dynamic
            // Add other necessary fields as per your requirements
        }
    }

}
﻿using System.Threading.Tasks;
using Infrastructure.Services.Detrack.DTOs;

namespace Infrastructure.Services.Detrack
{
    public interface IDetrackService
    {
        /// <summary>
        /// Creates a new job in the Detrack system.
        /// </summary>
        /// <param name="job">The job details to be sent to Detrack.</param>
        /// <returns>True if the job was successfully created, false otherwise.</returns>
        Task<bool> CreateDetrackJobAsync(DetrackJobDto job);
    }
}
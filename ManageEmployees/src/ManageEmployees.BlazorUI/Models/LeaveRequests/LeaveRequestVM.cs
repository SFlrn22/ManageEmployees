﻿using ManageEmployees.BlazorUI.Models.LeaveTypes;
using System.ComponentModel.DataAnnotations;

namespace ManageEmployees.BlazorUI.Models.LeaveRequests
{
    public class LeaveRequestVM
    {
        public int Id { get; set; }

        [Display(Name = "Date Requested")]
        public DateTime DateRequested { get; set; }
        [Display(Name = "Date Actioned")]
        public DateTime DateActioned { get; set; }
        [Display(Name = "Approval State")]
        public bool? Approved { get; set; }

        public bool Cancelled { get; set; }
        public LeaveTypeVM LeaveType { get; set; }
        public EmployeeVM Employee { get; set; }

        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }
        [Display(Name = "Leave Type")]
        public int LeaveTypeId { get; set; }
        [Display(Name = "Comments")]
        [MaxLength(300)]
        public string? RequestComments { get; set; }
    }
}

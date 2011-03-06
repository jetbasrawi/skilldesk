using System;
using System.Collections.Generic;
using SkillDesk.Domain.Entities;

namespace SkillDesk.Web.Models
{
    public class SkillsListViewModel
    {
        public PagingInfo PagingInfo;
        public List<SkillUiItem> SkillsToDisplay { get; set; }
        public string CurrentCategory { get; set; }
    }
}
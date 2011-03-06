using System.Text.RegularExpressions;
using SkillDesk.Domain.Entities;

namespace SkillDesk.Web.Models
{
    public class MoveUserSkillViewModel : SkillProfileViewModelBase
    {
        private static Regex _regex = new Regex("^(?i-msnx:(?<from>.*)/TO/(?<to>.*))", RegexOptions.CultureInvariant | RegexOptions.Compiled);

        public virtual string ActionName { get { return "Move"; } }

        public string FromPath {
            get {
                string pathPart = GetPathPart("from");
                return !string.IsNullOrEmpty(pathPart) ? pathPart : SkillPath ;
            }
        }
        
        public string ToPath { 
            get {
                string pathPart = GetPathPart("to");
                return pathPart;
            }
        }

        private string GetPathPart (string groupName) {
            Match match = _regex.Match(SkillPath);
            GroupCollection groupCollection = match.Groups;
            Group toGroup = groupCollection[groupName];
            return toGroup.Value;
        }
    }
}
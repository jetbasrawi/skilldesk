namespace SkillDesk.Web.Models
{
    public class CopyUserSkillViewModel : MoveUserSkillViewModel
    {
        public override string ActionName {
            get { return "Copy"; }
        }
    }
}
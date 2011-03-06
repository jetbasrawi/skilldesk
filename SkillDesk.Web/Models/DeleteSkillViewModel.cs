namespace SkillDesk.Web.Models
{
    public class DeleteSkillViewModel : SkillProfileViewModelBase
    {
        public DeleteSkillViewModel(string skillPath) {
            this.SkillPath = skillPath;
        }
    }
}
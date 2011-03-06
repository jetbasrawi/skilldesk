using System;
using System.Collections.Generic;
using System.Text;

namespace SkillDesk.Domain.Entities
{
    public class SkillPath
    {
        public readonly string FullPath;
        private string[] _splitPath;
        private string _parentPath;
        private List<SkillPath> _pathItems;
        private string _name;

        public SkillPath(string fullPath) {
            string path = null;

            if (fullPath != null) {
                path = fullPath.Trim();
            }

            FullPath = !string.IsNullOrEmpty(path) ? path : "/";
        }

        public string Name {
            get { return _name ?? (_name = GetName()); }
        }

        private string GetName() {
            string[] pathItems = SplitPath(FullPath);
            return pathItems[pathItems.Length - 1];
        }

        public string ParentPath {
            get { return _parentPath ?? (_parentPath = GetParentPath(FullPath)); }
        }

        public List<SkillPath> PathItems {
            get { return _pathItems ?? (_pathItems = new List<SkillPath>(GetPathItems(FullPath))); }
        }


        private string GetParentPath(string fullPath)
        {
            StringBuilder sb = new StringBuilder();
            string[] splitPath = SplitPath(fullPath);

            if (splitPath.Length > 0)
            {
                for (int i = 0; i < splitPath.Length - 1; i++)
                {
                    sb.AppendFormat("/{0}", splitPath[i]);
                }
                return sb.ToString();
            }

            return null;
        }
        
        private IEnumerable<SkillPath> GetPathItems(string pathToParse)
        {
            string[] splitPath = SplitPath(FullPath);

            StringBuilder pathBuilder;

            for (int i = 0; i < splitPath.Length; i++) {
                pathBuilder = new StringBuilder();
                
                for (int j = 0; j < i+1; j++) {
                    pathBuilder.AppendFormat("/{0}", splitPath[j]);
                }

                yield return new SkillPath(pathBuilder.ToString());
            }
        }

        private string[] SplitPath(string path)
        {
            return _splitPath ?? (_splitPath = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries));
        }
    }
}
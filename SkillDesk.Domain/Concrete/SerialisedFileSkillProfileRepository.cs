﻿using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using SkillDesk.Domain.Abstract;
using SkillDesk.Domain.Entities;

namespace SkillDesk.Domain.Concrete
{
    public class SerialisedFileSkillProfileRepository : ISkillProfileRepository 
    {
        const string FileName = @"C:\SkillsDB\SkillProfile.dat";
        
        public void Save(SkillProfile skillProfile)
        {
            using (Stream stream = File.Open(FileName, FileMode.Create))
            {
                XmlSerializer xs = new XmlSerializer(typeof(SkillProfile));
                xs.Serialize(stream, skillProfile);
                stream.Flush();
                stream.Close();
            }
        }

        public SkillProfile Load()
        {
            if (!File.Exists(FileName))
                return new SkillProfile();
            SkillProfile skillProfile;
            using (Stream stream = File.Open(FileName, FileMode.Open))
            {
                XmlSerializer xs = new XmlSerializer(typeof(SkillProfile));
                skillProfile = (SkillProfile) xs.Deserialize(stream);
                stream.Close();
            }
            return skillProfile;
        }

    }
}
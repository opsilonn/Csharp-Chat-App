using System;


namespace Back_Communication
{
    [Serializable]
    public class Member
    {
        private Structure _structure;
        private string _nameProfile;

        public Member(Structure structure, string nameProfileAdded)
        {
            this.structure = structure;
            this.nameProfile = nameProfileAdded;
        }


        public Structure structure
        {
            get { return _structure; }
            set { _structure = value; }
        }
        public string nameProfile
        {
            get { return _nameProfile; }
            set { _nameProfile = value; }
        }


        public override string ToString()
        {
            return "Member named : " + nameProfile + " into Structure " + structure.name;
        }
    }
}
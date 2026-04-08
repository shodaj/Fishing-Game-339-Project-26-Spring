namespace Game339.Shared.Services.Implementation
{
    public class PlayerInfo
    {
        private string name;
        private int age;
        
        public string GetName()
        {
            return name;
        }
        
        public void SetName(string newName)
        {
            name = newName;
        }   
        
        public int GetAge()
        {
            return age;
        }
        
        public void SetAge(int newAge)
        {
            age = newAge;
        }

        public void AgeOneYear()
        {
            age++;
        }
    }
}
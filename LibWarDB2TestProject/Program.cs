using LibWarDB2;

namespace LibWarDB2TestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var wdc = new WDC3();
            /*wdc.Load("C:/Users/Kevin Bernau/Downloads/item.db2");
            wdc.Save("C:/Users/Kevin Bernau/Downloads/item_new.db2");*/
            wdc.Load("C:/Users/Kevin Bernau/Downloads/gameobjects.db2");
            //wdc.Save("C:/Users/Kevin Bernau/Downloads/gameobjects_new.db2");
        }
    }
}

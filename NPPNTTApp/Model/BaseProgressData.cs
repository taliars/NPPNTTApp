namespace NPPNTTApp.Model
{
    public class BaseProgressData
    {
        public int Item { get; }
        public int Total { get; }

        public BaseProgressData(int item, int total)
        {
            Item = item;
            Total = total;
        }
    }
}

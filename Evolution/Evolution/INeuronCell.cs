namespace Zene.Evolution
{
    public interface INeuronCell
    {
        public int GetOrder { get; }
        public int SetOrder { get; }

        public double GetValue(Lifeform lifeform);
        public void SetValue(Lifeform lifeform, double value);

        public void Activate(Lifeform lifeform);
    }
}

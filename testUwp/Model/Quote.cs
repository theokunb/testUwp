using System;

namespace testUwp.Model
{
    public class Quote
    {
        public DateTime Date { get; set; }
        public Valute Valute { get; set; }
    }

    public class Valute
    {
        public ValuteValue USD {  get; set; }
        public ValuteValue EUR { get; set; }
    }

    public class ValuteValue
    {
        public string Id { get; set; }
        public string CharCode { get; set; }
        public int Nominal {  get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
    }
}

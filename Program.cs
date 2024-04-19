namespace nepessegHome
{
    class Orszag
    {
        public string Orszagnev { get; private set; }
        public int Terulet { get; private set; }
        public int Nepesseg { get; private set; }
        public string Fovaros { get; private set; }
        public int FovarosNepesseg { get; private set; }
        public float Nepsuruseg { get; private set; }
        public bool Fovaros_30 { get; private set; }
        public Orszag(string sor)
        {
            string[] tmp = sor.Split(';');
            Orszagnev = tmp[0];
            Terulet = int.Parse(tmp[1]);
            if (tmp[2].ToString().Contains("g"))
            {
                string nep = tmp[2].Replace("g", "0000");
                Nepesseg = int.Parse(nep);
            }
            else 
            {
                Nepesseg = int.Parse(tmp[2]);
            }
            
            Fovaros = tmp[3];
            FovarosNepesseg = int.Parse(tmp[4]);
            Nepsuruseg =(float)Nepesseg / Terulet;
            if ((float)FovarosNepesseg * 1000 / Nepesseg > 0.3) Fovaros_30 = true;
        }

        public override string ToString()
        {
            return "";
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Orszag> orszagok = new List<Orszag>();
            StreamReader sr = File.OpenText("adatok-utf8.txt");
            sr.ReadLine();
            while(!sr.EndOfStream) orszagok.Add(new Orszag(sr.ReadLine()));
            int n = orszagok.Count;
            Console.WriteLine("4. feladat:");
            Console.WriteLine($"A beolvasott országok száma: {n}\n");
            Console.WriteLine("5. feladat:");
            foreach (var i in orszagok) if(i.Orszagnev=="Kína") Console.WriteLine($"Kína népsűrűsége: {(int)(i.Nepsuruseg)} fő/km^2.\n");
            int kina = 0, india = 0, kulonbseg = 0;
            foreach (var i in orszagok) if (i.Orszagnev == "Kína") kina = i.Nepesseg;
            foreach (var i in orszagok) if (i.Orszagnev == "India") india = i.Nepesseg;
            kulonbseg = kina - india;
            Console.WriteLine("6. feladat:");
            Console.WriteLine($"Kínában a lakosság {kulonbseg} fővel vol több.\n");
            Console.WriteLine("7. feladat:");
            int max = 0;
            for (int i = 0; i < n; i++)
            {
                if (orszagok[max].Nepesseg < orszagok[i].Nepesseg && orszagok[i].Orszagnev != "Kína" && orszagok[i].Orszagnev != "India") max = i;
                
            }
            Console.WriteLine($"A harmadik legnépesebb ország: {orszagok[max].Orszagnev}, a lakosság {orszagok[max].Nepesseg} fő.\n");
            Console.WriteLine("8. feladat");
            foreach (var i in orszagok) if (i.Fovaros_30 == true) Console.WriteLine($"{i.Orszagnev}");
        }
    }
}

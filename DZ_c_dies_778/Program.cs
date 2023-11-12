using System.ComponentModel.Design;

namespace DZ_c_dies_778
{
    internal class Program
    {
        public delegate void TemperatureExceededThreshold();
        public class CopperSmeltingControlSystem
        {
            public CopperTemperatureSensor CTSens { get; set; }
            public bool IsRunning { get; set; }
            public CopperSmeltingControlSystem() { IsRunning = false;CTSens = new CopperTemperatureSensor(); }
            public void Start()
            {
                IsRunning = true;
                
                Console.WriteLine(" Печь работает .");
                while(IsRunning)
                {                   
                    CTSens.GetTemperature();
                    if (CTSens.COPF.Temperature > 20000)
                    {
                        TET?.Invoke();
                        Stop();return;
                    }
                }
            }
            public void Stop()
            {
                IsRunning= false;
                Console.WriteLine(" Печь остановлена. ");
            }
            public event TemperatureExceededThreshold TET;
        }
        public interface ITemperatureSensor
        {
            public void GetTemperature();
        }
        public class CopperFurnace { 
             public double Temperature { get; set; }
            public CopperFurnace() { Temperature = 0; }
            public void MeltCopper()
            {

            }
        }
        public class CopperTemperatureSensor : ITemperatureSensor
        {
            public CopperFurnace COPF { get; set; }
            public CopperTemperatureSensor() { COPF = new CopperFurnace(); }
            public void GetTemperature()
            {
                Random random = new Random();
                COPF.Temperature = random.Next(20, 30000);
                Console.WriteLine(" Температура нынешняя : " + COPF.Temperature);
            }

        }

        public class Sotrudnik
        {
            public string Nome { get; set; }
            public string Incarico { get; set; }
            public int Salario { get; set; }
            public Sotrudnik(string N,string In,int Sa) {
                Nome = N; Incarico = In; Salario = Sa;
            }
            public void Mostra()
            {
                Console.WriteLine(" Имя сотрудника : " + Nome + "\n Должность : " + Incarico + "  \n Зарплата : " + Salario);
            }
        }
        public delegate void Rium(Object ob);
        public class GlavBuh
        {
            public event Rium RRR;

            public List<Sotrudnik> SOTRU { get;set; }
            public GlavBuh() { SOTRU = new List<Sotrudnik>(); }
            public void Mostrare()
            {
                Console.WriteLine(" Список всех сотрудников : \n");
                foreach(var SO in SOTRU)
                {
                    SO.Mostra();
                }
            }
            public void PlusSotr(Sotrudnik sot)
            {
                SOTRU.Add(sot);
            }
            public void NuovoLavoratore()
            {
                Console.WriteLine(" Введите имя сотрудника : ");
                string? N=Convert.ToString(Console.ReadLine());
                Console.WriteLine(" Введите должность : ");
                string? N2 = Convert.ToString(Console.ReadLine());
                Console.WriteLine(" Введите зарплату : ");
                int N3 = Convert.ToInt32(Console.ReadLine());
                Sotrudnik Sot = new Sotrudnik(N, N2, N3);
                SOTRU.Add(Sot);
            }
            public void Rimuovere()
            {
                Console.WriteLine(" Введите имя сотрудника : ");
                string? N = Convert.ToString(Console.ReadLine());
                var NANA = SOTRU.FirstOrDefault(b => b.Nome == N);
                if(NANA == null)
                {
                    Console.WriteLine(" Сотрудника нет с таким именем ");return;
                }
                SOTRU.Remove(NANA);
                RRR?.Invoke(this);
                Console.WriteLine(" Сотрудник удален ");
            }
            public void TuttiSalari()
            {
                int A = 0;
                foreach(var DA in SOTRU)
                {
                    A += DA.Salario;
                }
                Console.WriteLine(" Общая сумма зарплат : " + A);
            }
            public void SalarioMed()
            {
                double A = 0;
                foreach (var DA in SOTRU)
                {
                    A += DA.Salario;
                }
                A = A / SOTRU.Count();
                Console.WriteLine(" Средняя зарплата : " + A);
            }
            public void Cercare()
            {
                Console.WriteLine(" Введите зарплату : ");
                int IIIN = Convert.ToInt32(Console.ReadLine());
                var ZAR = SOTRU.Where(s => s.Salario >= IIIN).Select(s => s);
                Console.WriteLine(" Сотрудник с зарплатой выше " + IIIN + '\n');
                foreach(var D in ZAR)
                {
                    D.Mostra();
                }
            }
            
        }
        
        static void Main(string[] args)
        {
            CopperSmeltingControlSystem COPPP = new CopperSmeltingControlSystem();
            COPPP.Start();

            Sotrudnik sotr1 = new Sotrudnik("AAAA AAAA", "GLav glav", 230000);
            Sotrudnik sotr2 = new Sotrudnik("BBBB BBBB", "lavoratore ", 120000);
            Sotrudnik sotr3 = new Sotrudnik("CCCC CCCC", "economist", 30000);
            Sotrudnik sotr4 = new Sotrudnik("DDDD DDDD", "politolog", 70000);
            Sotrudnik sotr5 = new Sotrudnik("EEEE EEEE", "voenkor", 80000);

            GlavBuh GB=new GlavBuh();
            GB.PlusSotr(sotr1);
            GB.PlusSotr(sotr2);
            GB.PlusSotr(sotr3);
            GB.PlusSotr(sotr4);
            GB.PlusSotr(sotr5);


                while (true)
                {
                    Console.WriteLine("1. Создание нового сотрудника с указанием имени, должности и зарплаты." +
                        "\n2. Просмотр списка всех сотрудников." +
                        "\n3. Увольнение сотрудника по его имени." +
                        "\n4. Подсчет общей суммы зарплаты всех сотрудников." +
                        "\n5. Подсчет средней зарплаты среди всех сотрудников." +
                        "\n6. Поиск сотрудников с зарплатой выше заданного значения." +
                        "\n7. Выход. ");
                    int v = Convert.ToInt32(Console.ReadLine());
                    switch (v)
                    {
                        case 1:GB.NuovoLavoratore(); break;
                        case 2: GB.Mostrare(); break;
                        case 3: GB.Rimuovere(); break;
                        case 4:GB.TuttiSalari(); break;
                        case 5:GB.SalarioMed(); break;
                        case 6: GB.Cercare(); break;
                        case 7: return;
                        default: break;
                    }
                }
            
        }

    }

}
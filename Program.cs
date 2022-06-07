// See https://aka.ms/new-console-template for more information
using System.Text;
namespace PCScore {
	class Program {
		static void Main() {
			Console.WriteLine("3DBenchmark");
			Console.WriteLine("==============\n");
			Console.WriteLine("Main Menu");
			Console.WriteLine("1. Find parts for target score.");
			Console.WriteLine("2. Calculate score from parts.");

			int choice = Convert.ToInt32(Console.ReadLine());
			
			if(choice == 1){
				targetScore();
			}else if(choice == 2){
				calcScore();
			}
			
		}
		
		static void calcScore(){
			List<string> cpuInfo = getCPU();
			List<string> gpuInfo = getGPU();
			
			int counter = 0;
			foreach(string cpu in cpuInfo){
				string[] info = cpu.Split(",");
				Console.WriteLine(counter + ". " + info[0]);
				counter++;
			}
			Console.WriteLine("Enter CPU: ");
			int cpuIndex = Convert.ToInt32(Console.ReadLine());

			counter = 0;
			foreach (string gpu in gpuInfo)
			{
				string[] info = gpu.Split(",");
				Console.WriteLine(counter + ". " + info[0]);
				counter++;
			}
			Console.WriteLine("Enter GPU: ");
			int gpuIndex = Convert.ToInt32(Console.ReadLine());
			
			int cpuScore = Convert.ToInt32(cpuInfo[cpuIndex].Split(",")[1]);
			int gpuScore = Convert.ToInt32(gpuInfo[gpuIndex].Split(",")[1]);
			
			Console.WriteLine(calculate(cpuScore, gpuScore));
			
		}
		
		static void targetScore(){
			Console.WriteLine("Enter target score: ");
			int target = Convert.ToInt32(Console.ReadLine());

			List<string> cpuInfo = getCPU();
			List<string> gpuInfo = getGPU();

			StringBuilder closest = new StringBuilder("");

			foreach (string cpu in cpuInfo)
			{
				foreach (string gpu in gpuInfo)
				{
					int cpuScore = Convert.ToInt32(cpu.Split(",")[1]);
					int gpuScore = Convert.ToInt32(gpu.Split(",")[1]);
					int score = calculate(cpuScore, gpuScore);
					if (score >= target && score <= target + 100)
					{
						display(score, cpu.Split(",")[0], gpu.Split(",")[0]);
					}
				}
			}
		}
		
		static void display(int score, string cpu, string gpu){
			Console.WriteLine(score + " | " + cpu + " | " + gpu);
		}
		
		static int calculate(int cpu, int gpu){
			double gpud = Convert.ToDouble(gpu);
			double cpud = Convert.ToDouble(cpu);
			int score = Convert.ToInt32(Math.Floor(1/((0.85/gpud)+(0.15/cpud))));
			
			return score;
		}
		
		static List<string> getCPU() {
			List<string> data = new List<string>();
			
			var lines = File.ReadAllLines("cpu.txt");
			
			foreach (var line in lines){
				data.Add(line);
			}
			
			return data;
		}

		static List<string> getGPU()
		{
			List<string> data = new List<string>();

			var lines = File.ReadAllLines("gpu.txt");

			foreach (var line in lines)
			{
				data.Add(line);
			}

			return data;
		}
	}
}

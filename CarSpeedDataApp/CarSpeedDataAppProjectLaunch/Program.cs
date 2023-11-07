using System.Diagnostics;

namespace CarSpeedDataAppProjectLaunch
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var basePath = Directory.GetCurrentDirectory();
			DirectoryInfo networkDir = new DirectoryInfo(basePath);
			var path = networkDir.Parent.Parent.Parent.Parent.Parent.ToString();

			var carSpeedAppPath = Path.Combine(path, "Deployment Package", "CarSpeedDataAppCompiled", "CarSpeedDataApp.exe");

			if (File.Exists(carSpeedAppPath))
			{
				var carSpeedAppProcess = new Process();
				carSpeedAppProcess.StartInfo.FileName = carSpeedAppPath;
				carSpeedAppProcess.StartInfo.WorkingDirectory =
					Path.GetDirectoryName(carSpeedAppPath);
				carSpeedAppProcess.Start();
			}

			var startInfo = new ProcessStartInfo

			{
				FileName = "npm",
				Arguments = "start",
				WorkingDirectory = Path.Combine(basePath, "car-speed-data-app"),
				UseShellExecute = true,
			};

			Process.Start(startInfo);
		}
	}
}
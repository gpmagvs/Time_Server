using Microsoft.Extensions.Logging.EventLog;
using System.Diagnostics;

var currentProcess = Process.GetCurrentProcess();
var _process_name = currentProcess.ProcessName;
var _process_id = currentProcess.Id;

Log($"Time Server Start-[{_process_id}]");
var porcessList = Process.GetProcessesByName(_process_name);
if (porcessList.Count() > 1)
{
    var otherProcess = porcessList.FirstOrDefault(pro => pro.Id != _process_id);
    var idOfOtherProcess = otherProcess.Id;
    otherProcess.Kill();
    Log($"Detect same program running...Process-{idOfOtherProcess} is killed");
}



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.AddEventLog(new EventLogSettings()
{
    SourceName = "SysTimeServer",
    LogName = "Application"
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", (ILogger<Program> logger, HttpContext context) =>
{
    var msg = $"{context.Connection.RemoteIpAddress?.ToString()} call api to get server system time.";
    Log(msg);
    return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
}).WithName("");

app.Run();


void Log(string msg)
{
    using (StreamWriter sw = new StreamWriter($"{Environment.CurrentDirectory}/Log.txt", true))
    {
        sw.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")} [{_process_id}] {msg}");
    }
    Console.WriteLine(msg);
}
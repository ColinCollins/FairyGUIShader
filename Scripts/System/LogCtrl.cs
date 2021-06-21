
// 控制台输出模块  --------- list model

public enum LogType 
{
	NONE = -1,
	SUCCESS,
	WARNING,
	ERROR
}

public static class LogCtrl
{
	// console debug model
	private static MainPanel _handle;

	public static void Init(MainPanel main) 
	{
		_handle = main;
		_handle.ClearLog();
	}

	public static void Success(string msg) 
	{
		_handle.AddLog(msg, LogType.SUCCESS);
	}

	// -------------- temporay
	public static void Warning(string msg) 
	{
		_handle.AddLog(msg, LogType.WARNING);
	}

}

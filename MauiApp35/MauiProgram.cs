﻿namespace MauiApp35;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
	
		var builder = MauiApp.CreateBuilder()
	
			.UseMauiApp<App>()
			
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		return builder.Build();
	}
}

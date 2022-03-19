using Android.Graphics;
using Plugin.LocalNotification;
using SkiaSharp;
using SkiaSharp.Views.Android;
using System.Diagnostics;


namespace MauiApp35;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();

	}
	string fullPath;
	private async void AddImage(object sender, EventArgs e)
	{
		var readStatus = Permissions.RequestAsync<Permissions.Photos>().Result;
		if (readStatus == PermissionStatus.Granted)
		{
			Debug.WriteLine("okuma yetkisi alındı");
			var result = await FilePicker.PickAsync();
			image.Source = result.FullPath;
			fullPath = result.FullPath;
			selector.IsVisible = false;
		}



		//count++;
		//CounterLabel.Text = $"Current count: {count}";

		//SemanticScreenReader.Announce(CounterLabel.Text);
	}
	private async void TestNotification(object sender, EventArgs args)
	{


		var notification = new NotificationRequest
		{
			NotificationId = 100,
			Title = "Test",
			Description = "Test Description",
			ReturningData = "Dummy data", // Returning data when tapped on notification.
			Schedule =
			{
				NotifyTime = DateTime.Now.AddSeconds(2) // Used for Scheduling local notification, if not specified notification will show immediately.
			}
		};
		await NotificationCenter.Current.Show(notification);
	}
	private void OnTapGestureRecognizerTapped(object sender, EventArgs args)
	{

		var byteArray = File.ReadAllBytes(fullPath);

	


		var bitmap = BitmapFactory.DecodeByteArray(byteArray, 0, byteArray.Length);
		var skiaBitmap = bitmap.ToSKBitmap();
		SKImageInfo info = skiaBitmap.Info;
		using (SKPaint paint = new SKPaint())
		{
			using (SKCanvas canvas = new SKCanvas(skiaBitmap))
			{

				paint.ColorFilter = SKColorFilter.CreateBlendMode(SKColor.FromHsl(0.2f, 0.9f, 0.19f, 150), SKBlendMode.ColorBurn);

				// SKColorFilter.CreateColorMatrix(new float[]
				// {
				//0.21f, 0.72f, 0.07f, 0, 0,
				//0.21f, 0.72f, 0.07f, 0, 0,
				//0.21f, 0.72f, 0.07f, 0, 0,
				//0,     0,     0,     1, 0
				// });

				canvas.DrawBitmap(skiaBitmap, info.Rect, paint: paint);
			}

		}

		var androidBitmap = skiaBitmap.ToBitmap();
		var bytex = androidBitmap.AsImageBytes(Bitmap.CompressFormat.Png);

		//AForge.Imaging.UnmanagedImage unmanagedImage =new AForge.Imaging.UnmanagedImage()
		////// apply the filter
		//var nBitmap=filter.Apply()

		//var ba=BitmapToByteArray(nBitmap);
		image.Source = ImageSource.FromStream(() => new MemoryStream(bytex));

		//First.IsVisible = false;
	}
	private async void TestAnim(object sender, EventArgs args)
	{


		await Task.WhenAny<bool>
			(
				 selector.RelRotateTo(360, 2000),
		 selector.FadeTo(0, 2000)
			);





	}
	private async void OnDragStarting(object sender, EventArgs args)
	{


	}
}


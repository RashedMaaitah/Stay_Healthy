using Microsoft.Toolkit.Uwp.Notifications;
using System.Media;
using System.Timers;
using Windows.UI.Notifications;

namespace Stay_Healthy;

public partial class Form1 : Form
{
    System.Timers.Timer? aTimer;
    public Form1()
    {
        InitializeComponent();
        SetTimer();
    }
    private void SetTimer()
    {
        aTimer = new System.Timers.Timer(new TimeSpan(0, 30, 0));
        aTimer.Elapsed += OnTimedEvent!;
        aTimer.AutoReset = true;
        aTimer.Start();
    }

    private void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        ShowToastNotification();
        PlayCustomAudio();
    }
    private static void ShowToastNotification()
    {
        // Audio
        ToastAudio audio = new()
        {
            Silent = true,
        };

        // Inline Image
        var imageUri = new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "life.jpg"));

        // Logo Image
        var logoUri = new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "R-Letter.png"));

        // Create the toast content
        var toastContent = new ToastContentBuilder()
            .AddText("Stay Healthy!")
            .AddText("Get up and move it's been 30 min!")
            .AddAppLogoOverride(new Uri("ms-appdata:///local/Andrew.jpg"), ToastGenericAppLogoCrop.Default)
            .AddAudio(audio)
            .AddInlineImage(imageUri)
            .AddAppLogoOverride(logoUri)
            .GetToastContent();

        // Create the toast notification
        var toast = new ToastNotification(toastContent.GetXml());

        // Show the toast notification
        ToastNotificationManagerCompat.CreateToastNotifier().Show(toast);
    }
    private static void PlayCustomAudio()
    {
        try
        {
            // Ensure the file path is correct
            string audioFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TheGoodBadSound.wav");

            using SoundPlayer player = new SoundPlayer(audioFilePath);
            player.Play();

        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error playing audio: {ex.Message}");
        }
    }

    private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        this.Show();
        notifyIcon1.Visible = false;
        WindowState = FormWindowState.Normal;
    }

    private void Form1_Resize(object sender, EventArgs e)
    {
        if (WindowState == FormWindowState.Minimized)
        {
            this.Hide();
            notifyIcon1.Visible = true;

        }
        else if (WindowState == FormWindowState.Normal)
        {
            notifyIcon1.Visible = false;

        }

    }

    private void Form1_Load(object sender, EventArgs e)
    {
        notifyIcon1.Text = "Stay Healthy";
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }
}

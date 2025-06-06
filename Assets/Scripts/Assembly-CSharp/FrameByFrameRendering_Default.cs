using System;
using System.IO;
using UnityEngine;

[AddComponentMenu("RealToon/Tools/Frame By Frame Rendering/Frame By Frame Rendering (Default)")]
public class FrameByFrameRendering_Default : MonoBehaviour
{
	[Header("(Frame By Frame Rendering V1.0.0)")]
	[Header("Click 'Play' button to start render.")]
	[Space(20f)]
	[Header("==============================")]
	[Header("(Settings)")]
	[Space(5f)]
	[Tooltip("Example Path: C:/TheNameOfTheFolder (Default folder name is Rendered Files and it will be created to your unity root project folder if this set to empty.")]
	public string PathFolder = "Rendered Files";

	[Tooltip("PNG File Name")]
	public string PNGFileName = "Frame";

	[Space(15f)]
	[Tooltip("Frame Rate (Set this to 24 if you want Film Style framerate.")]
	public int FrameRate = 24;

	[Tooltip("Start frame to start rendering.")]
	public int StartFrame;

	[Tooltip("End frame to end rendering.")]
	public int EndFrame = 100;

	[Space(15f)]
	[Tooltip("Render single frame or single image only, For Illustration or Art use.")]
	public bool SingleFrameRenderingMode;

	[Header("==============================")]
	[Header("(Information [Display Only] )")]
	[Space(5f)]
	[Tooltip("Display the current frame of your scene/play. (Display Only)")]
	public int CurrentFrame;

	[Tooltip("Display the information of the operation or rendering. (Display Only)")]
	public string info = string.Empty;

	private bool StartRendering;

	private bool StartFrameCheck;

	private string CurrentRenderedFile = string.Empty;

	private int StartFrameCont;

	private int EndFrameCont = 100;

	private int FrameRateCont = 24;

	private string PathFolderCont = "Rendered Files";

	private string PNGFileNameCont = "Frame";

	private bool SingleFrameRenderingCont;

	private DirectoryInfo DirInfo;

	private void Start()
	{
		if (StartFrameCont <= -1)
		{
			StartFrameCheck = false;
			StartRendering = false;
			info = "Rendering has not started because 'Start Frame' value is less than 0.";
			Debug.LogError(info);
		}
		if (EndFrameCont <= 0)
		{
			StartFrameCheck = false;
			StartRendering = false;
			info = "Rendering has not started because 'End Frame' value is 0 or less than 0.";
			Debug.LogError(info);
		}
		if (FrameRateCont <= 0)
		{
			StartFrameCheck = false;
			StartRendering = false;
			info = "Rendering has not started because 'Frame Rate' value is 0 or less than 0.";
			Debug.LogError(info);
		}
		if (PNGFileName == string.Empty)
		{
			PNGFileName = "Frame";
			info = "File Name set to 'Frame' because the field is not set or empty.";
			Debug.LogError(info);
		}
		if (PathFolder == string.Empty)
		{
			PathFolder = "Rendered Files";
			info = "Folder Path set to 'Rendered Files' and will be created to your UNITY ROOT PROJECT FOLDER because the field is not set or empty.";
			Debug.LogError(info);
		}
		Time.captureFramerate = FrameRate;
		StartFrameCont = StartFrame;
		EndFrameCont = EndFrame;
		FrameRateCont = FrameRate;
		SingleFrameRenderingCont = SingleFrameRenderingMode;
		PathFolderCont = PathFolder;
		PNGFileNameCont = PNGFileName;
		DirInfo = new DirectoryInfo(PathFolder);
		if (!Directory.Exists(PathFolder))
		{
			Directory.CreateDirectory(PathFolder);
			info = "Folder '" + PathFolder + "' Has Been Created To Your Root Project Folder.";
			Debug.LogWarning(info);
		}
		if (!SingleFrameRenderingMode)
		{
			info = "Video/Animation Mode";
			Debug.LogWarning(info);
			if (DirInfo.GetFiles().Length != 0)
			{
				StartFrameCheck = false;
				StartRendering = false;
				info = "(Video/Animation Mode) Rendering not started because there are already rendered frames or files in this folder ('" + PathFolder + "'), Please empty this folder or make another folder by changing the Path Folder.";
				Debug.LogError(info);
			}
			else
			{
				StartFrameCheck = true;
				StartRendering = true;
			}
		}
		else
		{
			StartFrameCheck = true;
			StartRendering = true;
			info = "Picture or Single Frame Rendering Mode";
			Debug.LogWarning(info);
			EndFrameCont = 1;
		}
	}

	private void Update()
	{
		CurrentFrame = Time.frameCount - 1;
		StartFrame = StartFrameCont;
		EndFrame = EndFrameCont;
		SingleFrameRenderingMode = SingleFrameRenderingCont;
		PathFolder = PathFolderCont;
		PNGFileName = PNGFileNameCont;
		if (PathFolder == string.Empty)
		{
			PathFolder = "Rendered Files";
		}
		else
		{
			if (!StartFrameCheck)
			{
				return;
			}
			if (CurrentFrame == StartFrameCont)
			{
				info = "Rendering Has Started.";
				Debug.LogWarning(info);
				StartRendering = true;
			}
			if (StartRendering)
			{
				if (!SingleFrameRenderingMode)
				{
					ScreenCapture.CaptureScreenshot(info = (CurrentRenderedFile = string.Format("{0}/" + PNGFileNameCont + " {1:D04}.png", PathFolderCont, CurrentFrame)));
				}
				else
				{
					ScreenCapture.CaptureScreenshot(info = (CurrentRenderedFile = string.Format("{0}/" + PNGFileNameCont + " " + DateTime.Now.ToString("hh_mm_ss") + ".png", PathFolderCont, CurrentFrame)));
				}
				if (CurrentFrame == EndFrame)
				{
					info = "Rendering Has Ended. [Last Rendered File: " + CurrentRenderedFile + "]";
					Debug.LogWarning(info);
					StartRendering = false;
					StartFrameCheck = false;
				}
			}
		}
	}
}

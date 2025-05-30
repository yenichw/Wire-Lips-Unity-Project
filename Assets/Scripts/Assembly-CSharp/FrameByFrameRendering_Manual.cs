using System;
using System.IO;
using UnityEngine;

[AddComponentMenu("RealToon/Tools/Frame By Frame Rendering/Frame By Frame Rendering (Manual)")]
public class FrameByFrameRendering_Manual : MonoBehaviour
{
	[Header("(Frame By Frame Rendering V1.0.0)")]
	[Header("Click 'Play' & 'Render' button to start render one by one.")]
	[Space(20f)]
	[Tooltip("Current Frame Number")]
	public int FrameNumber;

	[Space(5f)]
	[Tooltip("Start Render")]
	public bool Render;

	[Header("==============================")]
	[Header("(Settings)")]
	[Space(5f)]
	[Tooltip("Example Path: C:/TheNameOfTheFolder (Default folder name is Rendered Files and it will be created to your unity root project folder if this set to empty.")]
	public string PathFolder = "Rendered Files";

	[Tooltip("PNG File Name")]
	public string PNGFileName = "Frame";

	[Space(15f)]
	[Tooltip("Render single frame or single image only, For Illustration or Art use.")]
	public bool PictureMode;

	[Header("==============================")]
	[Header("(Information [Display Only] )")]
	[Space(5f)]
	[Tooltip("Display the information of the operation or rendering. (Display Only)")]
	public int LastRenderedFrame;

	public string info = string.Empty;

	private string CurrentRenderedFile = string.Empty;

	private string PathFolderCont = "Rendered Files";

	private string PNGFileNameCont = "Frame";

	private int FrameNumberCont;

	private bool PictureModeCont;

	private bool PreventRender;

	private DirectoryInfo DirInfo;

	private void Start()
	{
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
		PictureModeCont = PictureMode;
		PathFolderCont = PathFolder;
		PNGFileNameCont = PNGFileName;
		FrameNumberCont = FrameNumber;
		DirInfo = new DirectoryInfo(PathFolder);
		if (!Directory.Exists(PathFolder))
		{
			Directory.CreateDirectory(PathFolder);
			info = "Folder '" + PathFolder + "' Has Been Created To Your Root Project Folder.";
			Debug.LogWarning(info);
		}
		if (!PictureMode)
		{
			info = "Frame by Frame Rendering Mode";
			Debug.LogWarning(info);
			if (DirInfo.GetFiles().Length != 0)
			{
				PreventRender = true;
				Render = false;
				info = "(Frame by Frame Mode) Rendering not started because there are already rendered frames or files in this folder ('" + PathFolder + "'), Please empty this folder or make another folder by changing the Path Folder.";
				Debug.LogError(info);
			}
		}
		else
		{
			info = "Picture or Single Frame Rendering Mode";
			Debug.LogWarning(info);
		}
	}

	private void Update()
	{
		PictureMode = PictureModeCont;
		PathFolder = PathFolderCont;
		PNGFileName = PNGFileNameCont;
		LastRenderedFrame = FrameNumberCont;
		if (FrameNumber <= 0)
		{
			FrameNumber = FrameNumberCont;
		}
		if (!PreventRender && Render)
		{
			if (!PictureMode)
			{
				ScreenCapture.CaptureScreenshot(CurrentRenderedFile = string.Format("{0}/" + PNGFileNameCont + " {1:D04}.png", PathFolderCont, FrameNumber));
				FrameNumber++;
				FrameNumberCont = FrameNumber;
				info = CurrentRenderedFile;
				Debug.LogWarning(info);
				Render = false;
			}
			else
			{
				ScreenCapture.CaptureScreenshot(CurrentRenderedFile = string.Format("{0}/" + PNGFileNameCont + " " + DateTime.Now.ToString("hh_mm_ss") + ".png", PathFolderCont, FrameNumber));
				info = CurrentRenderedFile;
				Debug.LogWarning(info);
				Render = false;
			}
		}
	}
}

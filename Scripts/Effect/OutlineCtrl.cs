using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class OutlineCtrl: IFilter
{
	private DisplayObject _target;

	#region Variables
	Material _outlineMat;
	public Shader MosaicShader = null;
	private Material MosaicMaterial = null;
	public int MosaicSize = 8;

	Material material
	{
		get
		{
			if (MosaicMaterial == null)
			{
				MosaicMaterial = new Material(MosaicShader);
				MosaicMaterial.hideFlags = HideFlags.HideAndDontSave;
			}
			return MosaicMaterial;
		}
	}

	#endregion

	public DisplayObject target
	{
		get { return _target; }
		set
		{
			_target = value;
			_target.EnterPaintingMode(1024, null);
			_target.onPaint += OnRenderImage;

			_outlineMat = new Material(ShaderConfig.GetShader("FairyGUI/OutlineUI"));
			_outlineMat.hideFlags = DisplayObject.hideFlags;
		}
	}

	public OutlineCtrl() 
	{
		UIConfig.depthSupportForPaintingMode = true;
		MosaicShader = Shader.Find("FairyGUI/OutlineUI");

		// Disable the image effect if the shader can't
		// run on the users graphics card
		if (!MosaicShader || !MosaicShader.isSupported)
			return;
	}

	public void Dispose()
	{
		_target.LeavePaintingMode(1024);
		_target.onPaint -= OnRenderImage;
		_target = null;

		if (Application.isPlaying)
			Material.Destroy(_outlineMat);
		else
			Material.DestroyImmediate(_outlineMat);

		if (MosaicMaterial)
			Material.DestroyImmediate(MosaicMaterial);
	}

	void IFilter.Update()
	{
#if UNITY_EDITOR
		if (Application.isPlaying != true)
		{
			MosaicShader = Shader.Find("FairyGUI/OutlineUI");
		}
#endif
	}

	//清理RT
	private void ClearRt(RenderTexture renderTexture)
	{
		RenderTexture rt = RenderTexture.active;
		RenderTexture.active = renderTexture;
		GL.Clear(true, true, Color.clear);
		RenderTexture.active = rt;
	}

	public void OnRenderImage() 
	{
		RenderTexture sourceTexture = (RenderTexture)_target.paintingGraphics.texture.nativeTexture;
		int rtW = sourceTexture.width;
		int rtH = sourceTexture.height;
		RenderTexture destination = RenderTexture.GetTemporary(rtW, rtH);
		
		Graphics.Blit(sourceTexture, destination);

		ClearRt(sourceTexture);
		RenderTexture.active = sourceTexture;

		material.SetFloat("_OutlineWidth", MainCtrl.Instance.OutlineProp.LineWidth);
		material.SetVector("_OutlineColor", MainCtrl.Instance.OutlineProp.LineColor);

		Graphics.Blit(destination, sourceTexture, material, 0);

		RenderTexture.ReleaseTemporary(destination);
	}
}

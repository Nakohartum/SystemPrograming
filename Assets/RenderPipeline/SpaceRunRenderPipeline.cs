using UnityEngine;
using UnityEngine.Rendering;

namespace RenderPipeline
{
    public class SpaceRunRenderPipeline : UnityEngine.Rendering.RenderPipeline
    {
        private readonly CameraRenderer _cameraRenderer = new CameraRenderer();
        protected override void Render(ScriptableRenderContext context, Camera[] cameras)
        {
            CamerasRender(context, cameras);
        }

        private void CamerasRender(ScriptableRenderContext context, Camera[] cameras)
        {
            foreach (var camera in cameras)
            {
                _cameraRenderer.Render(context, camera);
            }
        }
    }
}

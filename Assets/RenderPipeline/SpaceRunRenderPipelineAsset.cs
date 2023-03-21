using UnityEngine;
using UnityEngine.Rendering;

namespace RenderPipeline
{
    [CreateAssetMenu(fileName = nameof(SpaceRunRenderPipelineAsset), menuName = "Rendering/"+nameof(SpaceRunRenderPipelineAsset))]
    public class SpaceRunRenderPipelineAsset : RenderPipelineAsset
    {
        protected override UnityEngine.Rendering.RenderPipeline CreatePipeline()
        {
            return new SpaceRunRenderPipeline();
        }
    }
}
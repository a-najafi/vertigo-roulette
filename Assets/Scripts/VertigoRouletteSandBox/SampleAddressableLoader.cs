using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace VertigoRouletteSandbox
{

    public class SampleAddressableLoader : MonoBehaviour
    {
        [SerializeField] private AssetReferenceAtlasedSprite sampleAddressableSpriteFromAtlas_value;
        

        [HideInInspector, SerializeField] private Image sampleImage;
        
        #if UNITY_EDITOR

        private void OnValidate()
        {
            sampleImage = GetComponent<Image>();
        }
        
        #endif 

        public void Start()
        {
            sampleAddressableSpriteFromAtlas_value.LoadAssetAsync<Sprite>().Completed+= OnLoadComplete;
            //Addressables.LoadAssetAsync<Sprite>(sampleAddressableSpriteFromAtlas_value).Completed+= OnLoadComplete;
        }

        private void OnLoadComplete(AsyncOperationHandle<Sprite> loadedSprite)
        {
            sampleImage.sprite = loadedSprite.Result;
            sampleImage.type = Image.Type.Simple;
            sampleImage.useSpriteMesh = true;
            sampleImage.preserveAspect = true;
            
            
            
        }
    }

}
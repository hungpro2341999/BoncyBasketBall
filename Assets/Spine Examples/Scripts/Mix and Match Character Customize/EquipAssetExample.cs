/******************************************************************************
 * Spine Runtimes License Agreement
 * Last updated January 1, 2020. Replaces all prior versions.
 *
 * Copyright (c) 2013-2020, Esoteric Software LLC
 *
 * Integration of the Spine Runtimes into software or otherwise creating
 * derivative works of the Spine Runtimes is permitted under the terms and
 * conditions of Section 2 of the Spine Editor License Agreement:
 * http://esotericsoftware.com/spine-editor-license
 *
 * Otherwise, it is permitted to integrate the Spine Runtimes into software
 * or otherwise create derivative works of the Spine Runtimes (collectively,
 * "Products"), provided that each user of the Products must obtain their own
 * Spine Editor license and redistribution of the Products in any form must
 * include this license and copyright notice.
 *
 * THE SPINE RUNTIMES ARE PROVIDED BY ESOTERIC SOFTWARE LLC "AS IS" AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL ESOTERIC SOFTWARE LLC BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES,
 * BUSINESS INTERRUPTION, OR LOSS OF USE, DATA, OR PROFITS) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
 * THE SPINE RUNTIMES, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if Unity_Editor
using UnityEditor;
#endif

namespace Spine.Unity.Examples {
    [CreateAssetMenu(fileName = "Equipt", menuName = "Use")]
    [System.Serializable]
    public class EquipAssetExample : ScriptableObject {
        public int idItem;
		public EquipSystemExample.EquipType equipType;
		public Sprite sprite;
		public string description;
		public int yourStats;
        public Sprite[] listSprite;

#if Unity_Editor
        [ContextMenu("Load")]
        public void Load_Asset_Head()
        {
            LoadAssetHead();
            LoadAssetHand();
            LoadAssetLeg();



            List<Sprite> ImgHead = new List<Sprite>();
            List<InforItem> Hands = new List<InforItem>();
            var a = GameHelper.GetAllAssetAtPath<Sprite>(null, "Assets/Game/Leg");
            for (int i = 0; i < a.Count; i++)
            {
                ImgHead.Add(a[i]);
                i += 1;
            }

            for (int i = 0; i < ImgHead.Count; i++)
            {
                InforItem aa = new InforItem();
                aa.id = i;
                aa.Img = ImgHead[i];
                Hands.Add(aa);


            }

            for (int i = 0; i < ImgHead.Count; i++)
            {

                InforItem aa = new InforItem();
                aa.id = i;
                aa.Img = ImgHead[i];


                var s = aa.Img.name;



                if (s.StartsWith("0"))
                {
                    aa.type = TypeItem.Default;
                }
                else if (s.StartsWith("b") || s.StartsWith("s"))
                {

                    aa.type = TypeItem.Item;

                }
                else
                {
                    aa.type = TypeItem.FullItem;


                }
                Hands.Add(aa);

            }

            EditorUtility.SetDirty(this);
            //EditorUtility.SetDirty(Data);
            AssetDatabase.SaveAssets();
            // Data.Leg.Heads = Hands;
        }
        public void LoadAssetHead()
        {
            List<Sprite> img = new List<Sprite>();
            var a = GameHelper.GetAllAssetAtPath<Sprite>(null, "Assets/Game/Head");
            for (int i = 0; i < a.Count; i++)
            {
                img.Add(a[i]);
                i += 1;
            }


            var aa = GameHelper.GetAllAssetAtPath<EquipAssetExample>(null, "Assets/Game/AssetSkin/HeadAsset");

            for (int i = 0; i < aa.Count; i++)
            {

                aa[i].equipType = EquipSystemExample.EquipType.Head;
                aa[i].sprite = img[i];
            }
        }
        public void LoadAssetHand()
        {
            List<Sprite> img = new List<Sprite>();
            var a = GameHelper.GetAllAssetAtPath<Sprite>(null, "Assets/Game/Hand");
            for (int i = 0; i < a.Count; i++)
            {
                img.Add(a[i]);
                i += 1;
            }


            var aa = GameHelper.GetAllAssetAtPath<EquipAssetExample>(null, "Assets/Game/AssetSkin/HandAsset");



            for (int i = 0; i < aa.Count; i++)
            {

                aa[i].sprite = img[i];
                if (img[i].name.StartsWith("v"))
                {
                    aa[i].equipType = EquipSystemExample.EquipType.ItemHand;
                }
                else
                {
                    aa[i].equipType = EquipSystemExample.EquipType.Hand;
                }
            }
        }

        public void LoadAssetLeg()
        {
            List<Sprite> img = new List<Sprite>();
            var a = GameHelper.GetAllAssetAtPath<Sprite>(null, "Assets/Game/Leg");
            for (int i = 0; i < a.Count; i++)
            {
                img.Add(a[i]);
                i += 1;
            }


            var aa = GameHelper.GetAllAssetAtPath<EquipAssetExample>(null, "Assets/Game/AssetSkin/LegAsset");



            for (int i = 0; i < aa.Count; i++)
            {

                aa[i].sprite = img[i];
                if (img[i].name.StartsWith("s") || img[i].name.StartsWith("b"))
                {
                    aa[i].equipType = EquipSystemExample.EquipType.ItemLeg;
                }
                else
                {
                    aa[i].equipType = EquipSystemExample.EquipType.Leg;
                }
            }
        }
#endif

    }
}

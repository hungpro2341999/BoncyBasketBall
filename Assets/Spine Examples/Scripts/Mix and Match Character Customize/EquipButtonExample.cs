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

using UnityEngine.UI;

namespace Spine.Unity.Examples {
	public class EquipButtonExample : MonoBehaviour {
		public EquipAssetExample Hand;
        public EquipAssetExample ItemHand;
        public EquipAssetExample Head;
        public EquipAssetExample Leg;
        public EquipAssetExample ItemLeg;


        public EquipAssetExample[] HeadAsset;
        public EquipAssetExample[] HandAsset;
        public EquipAssetExample[] ItemHandAsset;
        public EquipAssetExample[] LegAsset;
        public EquipAssetExample[] ItemLegAsset;
        public EquipSystemExample equipSystem;
		public Image inventoryImage;

        private void Awake()
        {
            Init();
        }
        private void Update()
        {
          
        }
      
        public void Init()
        {
            var a = CtrlDataGame.Ins.AssetSkin.AssetHead;
            HeadAsset = a.ToArray();

            var aa = CtrlDataGame.Ins.AssetSkin.AssetHand;

            List<EquipAssetExample> AssetHand = new List<EquipAssetExample>();
            List<EquipAssetExample> AssetItemHand = new List<EquipAssetExample>();
            for (int i = 0; i < aa.Count; i++)
            {
                if(aa[i].equipType == EquipSystemExample.EquipType.Hand)
                {
                    AssetHand.Add(aa[i]);
                }
                else
                {
                    AssetItemHand.Add(aa[i]);
                }
            }

            var aaa = CtrlDataGame.Ins.AssetSkin.AssetLeg;
            List<EquipAssetExample> AssetLeg = new List<EquipAssetExample>();
            List<EquipAssetExample> AssetItemLeg = new List<EquipAssetExample>();

            for (int i = 0; i < aaa.Count; i++)
            {
                if (aaa[i].equipType == EquipSystemExample.EquipType.Leg)
                {
                    AssetLeg.Add(aaa[i]);
                }
                else
                {
                    AssetItemLeg.Add(aaa[i]);
                }
            }

            this.HandAsset = AssetHand.ToArray();
            this.ItemHandAsset = AssetItemHand.ToArray();
            this.LegAsset = AssetLeg.ToArray();
            this.ItemLegAsset = AssetItemLeg.ToArray();
        }
        void OnValidate () {
			MatchImage();
		}

		void MatchImage () {
		
		}

		void Start () {
			MatchImage();
            Init();
			
		}

        public void EquipCharacter(int id)
        {
            Head = HeadAsset[id];
            EquipCharacter();
        }

        public void EquipHand(int id)
        {
           

            for (int i = 0; i < HandAsset.Length; i++)
            {
                if (HandAsset[i].idItem == id)
                {
                    Debug.Log("id :" + HandAsset[i].name);
                    Hand = HandAsset[i];
                    break;
                }
            }

        }

        public void EquipItemHand(int id)
        {

            for (int i = 0; i < ItemHandAsset.Length; i++)
            {
                if (ItemHandAsset[i].idItem == id)
                {
                    
                    ItemHand = ItemHandAsset[i];
                    break;
                }
            }

          
        }

        public void EquipLeg(int id)
        {
            for(int i = 0; i < LegAsset.Length; i++)
            {
                if(LegAsset[i].idItem == id)
                {
                    Leg = LegAsset[i];
                    break;
                }
            }
          
        }

        public void EquipItemLeg(int id)
        {
            for (int i = 0; i < ItemLegAsset.Length; i++)
            {
                if (ItemLegAsset[i].idItem == id)
                {
                    ItemLeg = ItemLegAsset[i];
                    break;
                }
            }

          
        }
        public void EquipHead(int id)
        {

            Debug.Log(id + "Errror");
            Head = HeadAsset[id];
        }

        public void EquipHandNull()
        {
            Hand = CtrlDataGame.Ins.AssetSkin.Null_Hand;
        }
        public void EquipItemHandNull()
        {
            ItemHand = CtrlDataGame.Ins.AssetSkin.Null_Item_Hand;
        }

        public void EquipLegNull()
        {
            Leg = CtrlDataGame.Ins.AssetSkin.Null_Leg;
        }

        public void EquipItemLegNull()
        {
            ItemLeg = CtrlDataGame.Ins.AssetSkin.Null_Item_Leg;
        }

        public void EquipCharacter()
        {
            //  Hand = HandAsset[Random.Range(0, HandAsset.Length)];
            equipSystem.Equip(Head);
            Debug.Log("Asset  :" +Head.name);
            equipSystem.Equip(ItemHand);
            
            equipSystem.Equip(Leg);
            equipSystem.Equip(ItemLeg);
            equipSystem.Equip(Hand);
            

        }
        public void CpuCharacter()
        {
            var skin = VsScreen.SkinUse;
           

            for(int i = 0; i < skin.Length; i++)
            {
                Debug.Log("ID : "+i +"   "+ skin[i]);
            }

            EquipHead(skin[0]);

            EquipHand(skin[1]);
            EquipLeg(skin[3]);
            if (skin[3] != -1)
            {
                EquipItemHandNull();
            }
            if (skin[4] != -1)
            {
                EquipItemLegNull();
            }

            equipSystem.Equip(Head);
            equipSystem.Equip(Hand);
            equipSystem.Equip(ItemHand);
            equipSystem.Equip(Leg);
            equipSystem.Equip(ItemLeg);




        }
       

      

	}
}

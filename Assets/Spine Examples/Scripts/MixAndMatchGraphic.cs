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

using UnityEngine;
using Spine.Unity.AttachmentTools;
using System.Collections;

namespace Spine.Unity.Examples {

	// This is an example script that shows you how to change images on your skeleton using UnityEngine.Sprites.
	public class MixAndMatchGraphic : MonoBehaviour {

		#region Inspector
		[SpineSkin]
		public string baseSkinName = "base";
		public Material sourceMaterial; // This will be used as the basis for shader and material property settings.

		[Header("Head")]
		public Sprite HeadSprite;
		[SpineSlot] public string HeadSlot;
		[SpineAttachment(slotField:"Head", skinField:"Head")] public string HeadKey = "KeyHead";

		[Header("Hand")]
		public Sprite handSprite;
		[SpineSlot] public string gunSlot;
		[SpineAttachment(slotField:"Hand", skinField:"Hand")] public string handKey = "gun";

		[Header("HandItem")]
		public Sprite ItemHandSprite;
		[SpineSlot] public string ItemHandSlot;
		[SpineAttachment(slotField: "ItemHand", skinField: "ItemHand")] public string ItemHandKey = "ItemHand";

		[Header("Item_Leg")]
		public Sprite ItemLegSprite;
		[SpineSlot] public string ItemLegSlot;
		[SpineAttachment(slotField: "ItemLeg", skinField: "ItemLeg")] public string ItemLegKey = "ItemLeg";

		[Header("Leg")]
		public Sprite LegSprite;
		[SpineSlot] public string LegSlot;
		[SpineAttachment(slotField: "Leg", skinField: "Leg")] public string LegKey = "Leg";

		[Header("Runtime Repack Required!!")]
		public bool repack = true;

		[Header("Do not assign")]
		public Texture2D runtimeAtlas;
		public Material runtimeMaterial;
		#endregion



		Skin customSkin;

		private void OnEnable()
		{
			
		}
		void OnValidate () {
			if (sourceMaterial == null) {
				var skeletonGraphic = GetComponent<SkeletonGraphic>();
				if (skeletonGraphic != null)
					sourceMaterial = skeletonGraphic.SkeletonDataAsset.atlasAssets[0].PrimaryMaterial;
			}
		}
		void Start()
		{
			Apply();


		}
		

		[ContextMenu("Apply")]
		public void Apply () {
			var skeletonGraphic = GetComponent<SkeletonGraphic>();
			var skeleton = skeletonGraphic.Skeleton;

			// STEP 0: PREPARE SKINS
			// Let's prepare a new skin to be our custom skin with equips/customizations. We get a clone so our original skins are unaffected.
			customSkin = customSkin ?? new Skin("custom skin"); // This requires that all customizations are done with skin placeholders defined in Spine.
			//customSkin = customSkin ?? skeleton.UnshareSkin(true, false, skeletonAnimation.AnimationState); // use this if you are not customizing on the default skin and don't plan to remove
			// Next let's get the skin that contains our source attachments. These are the attachments that
			var baseSkin = skeleton.Data.FindSkin(baseSkinName);

			// STEP 1: "EQUIP" ITEMS USING SPRITES
			// STEP 1.1 Find the original attachment.
			// Step 1.2 Get a clone of the original attachment.
			// Step 1.3 Apply the Sprite image to it.
			// Step 1.4 Add the remapped clone to the new custom skin.

			// Let's do this for the visor.
			int visorSlotIndex = skeleton.FindSlotIndex(HeadSlot); // You can access GetAttachment and SetAttachment via string, but caching the slotIndex is faster.
			Attachment baseAttachment = baseSkin.GetAttachment(visorSlotIndex, HeadKey); // STEP 1.1
			Attachment newAttachment = baseAttachment.GetRemappedClone(HeadSprite, sourceMaterial); // STEP 1.2 - 1.3
			customSkin.SetAttachment(visorSlotIndex, HeadKey, newAttachment); // STEP 1.4

			// And now for the gun.
			int gunSlotIndex = skeleton.FindSlotIndex(gunSlot);
			Attachment baseGun = baseSkin.GetAttachment(gunSlotIndex, handKey); // STEP 1.1
			Attachment newGun = baseGun.GetRemappedClone(handSprite, sourceMaterial); // STEP 1.2 - 1.3
			if (newGun != null) customSkin.SetAttachment(gunSlotIndex, handKey, newGun); // STEP 1.4
																						 // add Item Hand
			int ItemHandSlotIndex = skeleton.FindSlotIndex(ItemHandSlot);
			Attachment baseItemHand = baseSkin.GetAttachment(ItemHandSlotIndex, ItemHandKey); // STEP 1.1
			Attachment newItemHand = baseItemHand.GetRemappedClone(ItemHandSprite, sourceMaterial); // STEP 1.2 - 1.3

			if (newItemHand != null) customSkin.SetAttachment(ItemHandSlotIndex, ItemHandKey, newItemHand); // STEP 1.4


			//
		    //	customSkin.RemoveAttachment(ItemHandSlotIndex, ItemHandKey);
			// add Item leg
			int ItemLegSlotIndex = skeleton.FindSlotIndex(ItemLegSlot);
			Attachment baseItemLeg = baseSkin.GetAttachment(ItemLegSlotIndex, ItemLegKey); // STEP 1.1
			Attachment newItemLeg = baseItemLeg.GetRemappedClone(ItemLegSprite, sourceMaterial); // STEP 1.2 - 1.3
			if (newItemLeg != null) customSkin.SetAttachment(ItemLegSlotIndex, ItemLegKey, newItemLeg); // STEP 1.4

			// add Leg

			int LegSlotIndex = skeleton.FindSlotIndex(LegSlot);
			Attachment baseLeg = baseSkin.GetAttachment(LegSlotIndex, LegKey); // STEP 1.1
			Attachment newLeg = baseLeg.GetRemappedClone(LegSprite, sourceMaterial); // STEP 1.2 - 1.3
			if (newLeg != null) customSkin.SetAttachment(LegSlotIndex, LegKey, newLeg); // STEP 1.4
			
			//skeleton.FindSlot(ItemHandSlot).Attachment = null;




			// customSkin.RemoveAttachment(ItemHandSlotIndex, ItemHandKey); // To remove an item.
			//customSkin.Clear();
			// Use skin.Clear() To remove all customizations.
			// Customizations will fall back to the value in the default skin if it was defined there.
			// To prevent fallback from happening, make sure the key is not defined in the default skin.

			// STEP 3: APPLY AND CLEAN UP.
			// Recommended: REPACK THE CUSTOM SKIN TO MINIMIZE DRAW CALLS
			// 				Repacking requires that you set all source textures/sprites/atlases to be Read/Write enabled in the inspector.
			// 				Combine all the attachment sources into one skin. Usually this means the default skin and the custom skin.
			// 				call Skin.GetRepackedSkin to get a cloned skin with cloned attachments that all use one texture.
			if (repack)	{
				var repackedSkin = new Skin("repacked skin");
				repackedSkin.AddAttachments(skeleton.Data.DefaultSkin);
				repackedSkin.AddAttachments(customSkin);
				repackedSkin = repackedSkin.GetRepackedSkin("repacked skin", sourceMaterial, out runtimeMaterial, out runtimeAtlas);
				skeleton.SetSkin(repackedSkin);
			} else {
				skeleton.SetSkin(customSkin);
			}

			//skeleton.SetSlotsToSetupPose();
			skeleton.SetToSetupPose();
			skeletonGraphic.Update(0);
			skeletonGraphic.OverrideTexture = runtimeAtlas;

			Resources.UnloadUnusedAssets();
		}




		public void Attachment_Head(Sprite Img)
		{
			this.HeadSprite = Img;
			
		}

		public void Attachment_Hand(Sprite Img)
		{
			this.handSprite = Img;

		


		}
		public void Attachment_Item_Hand(Sprite Img)
		{
			this.ItemHandSprite = Img;
		}

		public void Attachment_Item_Leg(Sprite Img)
		{
			this.ItemLegSprite = Img;
		}
		public void Attachment_Leg(Sprite Img)
		{
			this.LegSprite = Img;
		}

		public void ApplySkin(int[] Skins)
		{
			HeadSprite = CtrlDataGame.Ins.Resource.Heads.Heads[Skins[0]].Img;
			handSprite = CtrlDataGame.Ins.Resource.Hands.Heads[Skins[1]].Img;
			if (Skins[2] != -1)
			{
				ItemHandSprite = CtrlDataGame.Ins.Resource.Hands.Heads[Skins[2]].Img;
			}
			else
			{
				ItemHandSprite = CtrlDataGame.Ins.Resource.Sprite_Null;
			}
			LegSprite = CtrlDataGame.Ins.Resource.Leg.Heads[Skins[3]].Img;

			if (Skins[4] != -1)
			{
				ItemLegSprite = CtrlDataGame.Ins.Resource.Leg.Heads[Skins[4]].Img;

			}
			else
			{
				ItemLegSprite = CtrlDataGame.Ins.Resource.Sprite_Null;

			}
			Apply();

		}

	}


}

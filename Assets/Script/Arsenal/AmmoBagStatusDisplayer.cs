using Script.Arsenal;
using Script.Base.Utils;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Assets.Script.Arsenal
{
    [RequireComponent(typeof(TMP_Text))]
    public class AmmoBagStatusDisplayer : MonoBehaviour
    {
        private TMP_Text _displayText;
        private AmmoBag _ammoBag;

        void Start()
        {
            _displayText = GetComponent<TMP_Text>();
            _ammoBag = FindObjectOfType<AmmoBag>();
        }

        void FixedUpdate()
        {
            if (!_ammoBag)
            {
                _displayText.text = string.Empty;
                return;
            }

            IEnumerable<string> ammoStatuses = _ammoBag.AmmoQuantity.Select(x => x.Key.Description() + ": " + x.Value);
            _displayText.text = string.Join('\n', ammoStatuses);
        }
    }
}
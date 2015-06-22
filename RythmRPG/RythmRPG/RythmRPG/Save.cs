using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG
{
    public class Save
    {
        /// <summary>
        /// The number of save
        /// </summary>
        public const int NB_SAVE = 3;
        /// <summary>
        /// Gets or sets the selected save.
        /// </summary>
        /// <value>
        /// The selected save.
        /// </value>
        public int SelectedSave { get; set; }
        /// <summary>
        /// Gets or sets the characters array.
        /// </summary>
        /// <value>
        /// The characters array.
        /// </value>
        public Characters[] CharactersArray { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Save"/> class.
        /// </summary>
        public Save()
        {
            CharactersArray = new Characters[NB_SAVE];
            for (int i = 0; i < NB_SAVE; i++)
            {
                this.CharactersArray[i] = new Characters();
            }
            this.SelectedSave = 0;
        }
    }
}

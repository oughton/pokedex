using System;

namespace Pokedex
{
    /// <summary>
    /// Describes a Pokemon.
    /// </summary>
    public class Pokemon
    {
        /// <summary>
        /// Creates a <see cref="Pokemon"/>.
        /// </summary>
        /// <param name="name">The Pokemon name.</param>
        /// <param name="description">A description of the Pokemon.</param>
        /// <param name="habitat">The habitat in which the Pokemon resides.</param>
        /// <param name="isLegendary">A flag indicating whether the Pokemon is legendary.</param>
        public Pokemon(string name, string description, string habitat, bool isLegendary)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (habitat == null)
                throw new ArgumentNullException(nameof(habitat));

            Name = name;
            Description = description ?? "";
            Habitat = habitat;
            IsLegendary = isLegendary;
        }

        /// <summary>
        /// The Pokemon name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// A description of the Pokemon.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The habitat in which the Pokemon resides.
        /// </summary>
        public string Habitat { get; }

        /// <summary>
        /// A flag indicating whether the Pokemon is legendary.
        /// </summary>
        public bool IsLegendary { get; }
    }
}

namespace LocalNetwork
{
    using System;

    public class Lambda
    {
        /// <summary>
        /// value of return number
        /// </summary>
        public int ReturnValue
        {
            get { return isRandom ? randomElement.Next(0, 100) : 0; }
        }

        /// <summary>
        /// random element
        /// </summary>
        private Random randomElement = new Random();

        /// <summary>
        /// check we need random number or not
        /// </summary>
        private bool isRandom;

        /// <summary>
        /// class constructor
        /// </summary>
        /// <param name="isRandom">does we need random number?</param>
        public Lambda(bool isRandom)
        {
            this.isRandom = isRandom;
        }
    }
}

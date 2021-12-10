namespace Ipfs.Engine.Cryptography
{
    class KeyInfo : IKey
    {
        public string Name { get; set; }
        public MultiHash Id { get; set; }
    }
}

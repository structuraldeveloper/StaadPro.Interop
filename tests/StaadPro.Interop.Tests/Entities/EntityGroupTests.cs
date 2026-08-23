using System.Collections.Generic;
using NUnit.Framework;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Tests.Entities
{
    [TestFixture]
    public class EntityGroupTests
    {
        [Test]
        public void EntityGroup_CreateAndConvert_MaintainsGroupIntegrity()
        {
            var beams = new List<Beam>
            {
                new Beam(1, new Node(1, 0, 0, 0), new Node(2, 1, 0, 0)),
                new Beam(2, new Node(2, 1, 0, 0), new Node(3, 2, 0, 0))
            };

            var group = new EntityGroup<Beam>
            {
                GroupName = "_BEAMS_MAIN",
                Entities = new HashSet<Beam>(beams)
            };

            Assert.AreEqual(EntityType.Beam, group.EntityType);
            Assert.AreEqual(2, group.Entities.Count);

            EntityGroup nonGen = EntityGroup<Beam>.ToNonGeneric(group);
            Assert.AreEqual("_BEAMS_MAIN", nonGen.GroupName);
            Assert.AreEqual(EntityType.Beam, nonGen.EntityType);
            Assert.IsTrue(nonGen.Entities.Contains(1));
            Assert.IsTrue(nonGen.Entities.Contains(2));
        }
    }
}

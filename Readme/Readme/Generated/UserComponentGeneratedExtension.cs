namespace Entitas {
    public partial class Entity {
        public UserComponent user { get { return (UserComponent)GetComponent(ComponentIds.User); } }

        public bool hasUser { get { return HasComponent(ComponentIds.User); } }

        public Entity AddUser(string newName, int newAge) {
            var componentPool = GetComponentPool(ComponentIds.User);
            var component = (UserComponent)(componentPool.Count > 0 ? componentPool.Pop() : new UserComponent());
            component.name = newName;
            component.age = newAge;
            return AddComponent(ComponentIds.User, component);
        }

        public Entity ReplaceUser(string newName, int newAge) {
            var componentPool = GetComponentPool(ComponentIds.User);
            var component = (UserComponent)(componentPool.Count > 0 ? componentPool.Pop() : new UserComponent());
            component.name = newName;
            component.age = newAge;
            ReplaceComponent(ComponentIds.User, component);
            return this;
        }

        public Entity RemoveUser() {
            return RemoveComponent(ComponentIds.User);
        }
    }

    public partial class Pool {
        public Entity userEntity { get { return GetGroup(Matcher.User).GetSingleEntity(); } }

        public UserComponent user { get { return userEntity.user; } }

        public bool hasUser { get { return userEntity != null; } }

        public Entity SetUser(string newName, int newAge) {
            if (hasUser) {
                throw new EntitasException("Could not set user!\n" + this + " already has an entity with UserComponent!",
                    "You should check if the pool already has a userEntity before setting it or use pool.ReplaceUser().");
            }
            var entity = CreateEntity();
            entity.AddUser(newName, newAge);
            return entity;
        }

        public Entity ReplaceUser(string newName, int newAge) {
            var entity = userEntity;
            if (entity == null) {
                entity = SetUser(newName, newAge);
            } else {
                entity.ReplaceUser(newName, newAge);
            }

            return entity;
        }

        public void RemoveUser() {
            DestroyEntity(userEntity);
        }
    }

    public partial class Matcher {
        static IMatcher _matcherUser;

        public static IMatcher User {
            get {
                if (_matcherUser == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.User);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherUser = matcher;
                }

                return _matcherUser;
            }
        }
    }
}

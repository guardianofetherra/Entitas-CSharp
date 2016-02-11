using Entitas;

namespace Entitas {
    public partial class Entity {
        public UIPositionComponent uIPosition { get { return (UIPositionComponent)GetComponent(UIComponentIds.UIPosition); } }

        public bool hasUIPosition { get { return HasComponent(UIComponentIds.UIPosition); } }

        public Entity AddUIPosition(int newX, int newY) {
            var componentPool = GetComponentPool(UIComponentIds.UIPosition);
            var component = (UIPositionComponent)(componentPool.Count > 0 ? componentPool.Pop() : new UIPositionComponent());
            component.x = newX;
            component.y = newY;
            return AddComponent(UIComponentIds.UIPosition, component);
        }

        public Entity ReplaceUIPosition(int newX, int newY) {
            var componentPool = GetComponentPool(UIComponentIds.UIPosition);
            var component = (UIPositionComponent)(componentPool.Count > 0 ? componentPool.Pop() : new UIPositionComponent());
            component.x = newX;
            component.y = newY;
            ReplaceComponent(UIComponentIds.UIPosition, component);
            return this;
        }

        public Entity RemoveUIPosition() {
            return RemoveComponent(UIComponentIds.UIPosition);
        }
    }
}

    public partial class UIMatcher {
        static IMatcher _matcherUIPosition;

        public static IMatcher UIPosition {
            get {
                if (_matcherUIPosition == null) {
                    var matcher = (Matcher)Matcher.AllOf(UIComponentIds.UIPosition);
                    matcher.componentNames = UIComponentIds.componentNames;
                    _matcherUIPosition = matcher;
                }

                return _matcherUIPosition;
            }
        }
    }

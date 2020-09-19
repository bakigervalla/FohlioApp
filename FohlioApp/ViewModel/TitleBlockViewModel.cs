using Fohlio.RevitReportsIntegration.Properties;

namespace Fohlio.RevitReportsIntegration.ViewModel
{
    public class TitleBlockViewModel
    {
        public TitleBlockViewModel(object id, string name)
        {
            Id = id;

            Name = name;
        }

        public object Id { get; }

        public string Name { get; }

        public static TitleBlockViewModel CreateEmpty() => new TitleBlockViewModel("123", Resources.EmptyTitleBlockName);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            return obj.GetType() == GetType() && Equals((TitleBlockViewModel) obj);
        }

        public override int GetHashCode() => Id.GetHashCode();

        protected bool Equals(TitleBlockViewModel other) => Id == other.Id;
    }
}
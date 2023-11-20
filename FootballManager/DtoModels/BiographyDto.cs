namespace FootballManager.DtoModels
{
    public class BiographyDto : BaseDtoModel
    {
        private string teamTitle;
        private string startContract;
        private string endContract;
        private string contractStatus;
        private int? playerId;
        private PlayerDto player;

        public string TeamTitle
        {
            get { return teamTitle; }
            set
            {
                teamTitle = value;
                OnPropertyChanged("TeamTitle");
            }
        }
        public string StartContract
        {
            get { return startContract; }
            set
            {
                startContract = value;
                OnPropertyChanged("StartContract");
            }
        }
        public string EndContract
        {
            get { return endContract; }
            set
            {
                endContract = value;
                OnPropertyChanged("EndContract");
            }
        }
        public string ContractStatus
        {
            get { return contractStatus; }
            set
            {
                contractStatus = value;
                OnPropertyChanged("ContractStatus");
            }
        }
        public int? PlayerId
        {
            get { return playerId; }
            set
            {
                playerId = value;
                OnPropertyChanged("PlayerId");
            }
        }
        public PlayerDto Player
        {
            get { return player; }
            set
            {
                player = value;
                OnPropertyChanged("Player");
            }
        }
    }
}

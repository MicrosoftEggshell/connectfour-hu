using Moq;

using EVAL.ConnectFour.Common;
using EVAL.ConnectFour.Persistence;
using EVAL.ConnectFour.Model;

namespace EVAL.ConnectFour.Test
{
    [TestClass]
    public class ConnectFourModelTest
    {
        private bool _expectDraw = false;
        private bool _ignoreMove = true;
        private Position? _expectedMove = null;
        private Position[]? _expectedWinPos = null;

        private ConnectFourModel _model = null!; // tesztelt model
        private ConnectFourBoard _mockedBoard = null!; // tábla a mockhoz
        private Mock<IConnectFourDataAccess> _mock = null!; // adatelérés mock

        [TestInitialize]
        public void Initialize()
        {
            // tábla feltöltése a mockol perzisztenciához
            _mockedBoard = new ConnectFourBoard(10, 10);
            _mockedBoard.Insert(0, PlayerColour.X);
            _mockedBoard.Insert(9, PlayerColour.O);
            _mockedBoard.Insert(5, PlayerColour.X);
            _mockedBoard.Insert(6, PlayerColour.O);
            _mockedBoard.Insert(6, PlayerColour.X);
            _mockedBoard.Insert(7, PlayerColour.O);
            _mockedBoard.Insert(8, PlayerColour.X);
            _mockedBoard.Insert(9, PlayerColour.O);
            _mockedBoard.Insert(8, PlayerColour.X);
            _mockedBoard.Insert(0, PlayerColour.O);

            // perzisztencia betöltés metódusának a definiálása
            _mock = new Mock<IConnectFourDataAccess>();
            _mock.Setup(mock => mock.LoadAsync(It.IsAny<string>()))
                .Returns(() => Task.FromResult(_mockedBoard));

            // model példányosítása mockból
            _model = new ConnectFourModel(ConnectFourModel.LoadBoardAsync(string.Empty, _mock.Object).Result);

            _model.GameEvent += UniversalGameEventHandler;
            _model.TimeAdvanced += UniversalGameEventHandler;

            _model.Replay();
            _ignoreMove = false;
        }

        private void UniversalGameEventHandler(object? sender, ConnectFourEventArgs e)
        {
            if (
            sender is not ConnectFourModel model ||
            model != _model
                )
            {
                return;
            }
            switch (e.EventType)
            {
                case ConnectFourEvent.TIME_ELAPSED:
                    Model_TimeElapsed(e.GameTime);
                    break;
                case ConnectFourEvent.PLACEMENT:
                    Model_Move(e.Position);
                    break;
                case ConnectFourEvent.WIN:
                    Model_Win(e.Winner);
                    break;
                case ConnectFourEvent.DRAW:
                    model_Draw();
                    break;
                default:
                    throw new ArgumentException($"Ismeretlen eseménytípus: \"${e.EventType}\".");
            }
        }

        private void model_Draw()
        {
            Assert.IsTrue(_expectDraw);
            for(int i = 0; i < _model.Columns; ++i) {
                try
                {
                    _model.Place(i);
                    Assert.IsTrue(false);
                }
                catch { }
            }
        }

        private void Model_Win(Position[]? winner)
        {
            Assert.IsNotNull(_expectedWinPos);
            Assert.IsNotNull(winner);
            Assert.AreEqual(winner.Length, _expectedWinPos.Length);
            for(int i = 0; i < winner.Length; ++i)
            {
                Assert.AreEqual(_expectedWinPos[i], winner[i]);
            }
        }

        private void Model_Move(Position? position)
        {
            if(_ignoreMove)
            {
                return;
            }
            Assert.IsNotNull(_expectedMove);
            Assert.IsNotNull(position);
            Assert.AreEqual(position, _expectedMove);
            Assert.AreEqual(_model.ColumnHeight(position.X), position.Y);
        }

        private void Model_TimeElapsed(TimeSpan gameTime)
        {
            Assert.IsTrue(gameTime.TotalMilliseconds >= 0);
        }

        [TestMethod]
        public void WinTest()
        {
            foreach (int i in new int[] { 9, 8, 8, 7 })
            {
                int prevHeight = _model.ColumnHeight(i);
                _expectedMove = new Position(i, _model.ColumnHeight(i));
                _model.Place(i);
                Assert.AreEqual(prevHeight + 1, _model.ColumnHeight(i));
            }
            _expectedMove = new Position(7, 2);
            _expectedWinPos = new Position[] {
                new Position(5, 0),
                new Position(6, 1),
                new Position(7, 2),
                new Position(8, 3)
            };
            _model.Place(7);
            Assert.IsFalse(_model.IsOngoing);
        }

        [TestMethod]
        public void DrawTest()
        {
            foreach (int i in new int[] {
                5, 5, 5, 5, 5, 5, 5, 5,       // X következik
                0, 0, 0, 0, 0, 0, 0,          // O következik
                9, 9, 9, 9, 9, 9,             // X következik
                1, 1, 1, 1, 1, 1, 1, 1,       // O következik
                2, 2, 2, 2, 2, 2, 2, 2, 2, 2, // X következik
                7, 7, 7, 7, 7, 7, 7, 7, 7,    // O következik
                3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
                6, 6, 6, 6, 6, 6, 6, 4,
                6, 4, 4, 4, 4, 4, 4, 8, 4, 8,
                4, 8, 8, 8, 8, 8, 8, 4
            })
            {
                int prevHeight = _model.ColumnHeight(i);
                _expectedMove = new Position(i, _model.ColumnHeight(i));
                _model.Place(i);
                Assert.AreEqual(prevHeight + 1, _model.ColumnHeight(i));
                Assert.IsTrue(_model.IsOngoing, i.ToString());
            }
            _ignoreMove = true;
            for (int i = 0; i < 9; ++i)
            {
                while (_model.ColumnHeight(i) < 10)
                    _model.Place(i);
            }
            _model.Place(9);
            _expectDraw = true;
            _model.Place(9);
            Assert.IsFalse(_model.IsOngoing);
        }
    }
}
namespace WSAD_Walker
{
    class Schuss
    {
        int left;
        int top;
        Program.Direction direction;

        public Schuss(int left, int top, Program.Direction direction)
        {
            this.direction = direction;
            switch (direction)
            {
                case Program.Direction.UP:
                    this.left = left;
                    this.top = --top;
                    break;
                case Program.Direction.DOWN:
                    this.left = left;
                    this.top = ++top;
                    break;
                case Program.Direction.LEFT:
                    this.left = --left;
                    this.top = top;
                    break;
                case Program.Direction.RIGHT:
                    this.left = ++left;
                    this.top = top;
                    break;
            }

        }

        public int Left => left;
        public int Top => top;
        public Program.Direction Direction => direction;

        public void Move()
        {
            switch (direction)
            {
                case Program.Direction.UP:
                    top--;
                    break;
                case Program.Direction.DOWN:
                    top++;
                    break;
                case Program.Direction.LEFT:
                    left--;
                    break;
                case Program.Direction.RIGHT:
                    left++;
                    break;
                default:
                    break;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPractice
{
    public class Queue
    {

        private int[] buffer;
        private int queue_size;
        protected int front;
        protected int rear;

        public Queue()
        {
            front = 0; rear = 0; queue_size = 10;
            buffer = new int[queue_size];
        }
        public Queue(int n)
        {
            front = 0; rear = 0; queue_size = n;
            buffer = new int[queue_size];
        }
        public void enqueue(int v)
        {
            if (rear < queue_size) buffer[rear++] = v;
            else if (compact()) buffer[rear++] = v;
            
        }
        public int dequeue()
        {
            if (front < rear) return buffer[front++];
            else
            {
                Console.WriteLine("Error:Queue Empty");
                return -1;
            }
        }
            
        private bool compact()
        {
            if(front == 0)
            {
                Console.WriteLine("Error:Queue overflow");
                return false;
            }
            else
            {
                for (int i = 0; i < rear - front; i++)
                    buffer[i] = buffer[i + front];
                rear = rear - front; front = 0;
                return true;
            }
        }

        private static void Main()
        {
            Queue Q2 = new Queue(4);
            Q2.enqueue(12); Q2.enqueue(18);
            int x = Q2.dequeue(); int y = Q2.dequeue();
            Console.Write("X = {0} Y={1}", x, y);
            Console.ReadLine();
        }

    }

}

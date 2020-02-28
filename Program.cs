using System;

namespace WassonLinkedListTest
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList myList = new LinkedList();
            myList.setup();

            Console.WriteLine("This program builds and manipulates a double linked list." +
                "\n\nCommands:" + 
                "\nF: Go to first" +
                "\nL: Go to last" +
                "\nN: Go to next" +
                "\nP: Go to previous" +
                "\nB: Insert before" +
                "\nA: Insert after" +
                "\nD: Delete current" +
                "\nE: Empty the list" +
                "\nR: Read names" +
                "\nX: Stop");

            String input = "";
            while (!input.StartsWith("X"))
            {
                input = Console.ReadLine().ToUpper();
                

                switch (input[0])
                {
                    case 'F':
                        myList.goToFirst();
                        break;

                    case 'L':
                        myList.goToLast();
                        break;

                    case 'N':
                        myList.nextNode();
                        break;

                    case 'P':
                        myList.prevNode();
                        break;

                    case 'B':
                        Console.WriteLine("\n Name your new node:");
                        String nameBefore = Console.ReadLine();
                        myList.addNodeBefore(nameBefore);
                        break;

                    case 'A':
                        Console.WriteLine("\n Name your new node:");
                        String nameAfter = Console.ReadLine();
                        myList.addNodeAfter(nameAfter);
                        break;

                    case 'U':
                        Console.WriteLine("Change name to:");

                        String newName = Console.ReadLine();
                        myList.updateNode(newName);
                        break;

                    case 'D':
                        myList.deleteCurrent();
                        break;

                    case 'E':
                        Console.WriteLine("Comfirm list deletion? (Y/N");

                        String confirm = Console.ReadLine().ToUpper();
                        if(confirm.StartsWith('Y'))
                        {
                            myList.deleteAll();
                            Console.WriteLine("List emptied.");
                        }
                        else
                        {
                            Console.WriteLine("Confirmation not accepted. List retained.");
                        }
                        break;

                    case 'R':
                        myList.printNodeNames();
                        break;

                    case 'X':
                        break;

                    default:
                        Console.WriteLine("Input not recognized.");
                        break;


                }
            }

        }
    }






    public class Node
    {
        public Node next;
        public Node prev;
        public String name;
    }

    public class LinkedList
    {
        private Node head = new Node();
        private Node tail = new Node();
        private Node current;

        public void setup()
        {
            head.next = tail;
            tail.prev = head;
            current = head;
        }
        public void goToFirst()
        {
            if (head.next != null && head.next != tail)
            {
                current = head.next;
                Console.WriteLine("First node: " + current.name);
            }
            else
            {
                current = head;
                Console.WriteLine("Empty List.");
            }
        }

        public void goToLast()
        {
            if(tail.prev != head)
            {
                current = tail.prev;
                Console.WriteLine("Last node: " + current.name);
            }
            else
            {
                current = head;
                Console.WriteLine("Empty List.");
            }
        }

        public void nextNode()
        {
            if(current.next != null)
            {
                current = current.next;
                if(current == tail)
                {
                    Console.WriteLine("Moved to end of list.");
                }
                else
                {
                    Console.WriteLine("Current node: " + current.name);
                }
            }
            else
            {
                Console.WriteLine("End of List");
            }
        }

        public void prevNode()
        {
            if(current.prev != null)
            {
                current = current.prev;
                if (current == head)
                {
                    Console.WriteLine("Moved to start of list.");
                }
                else
                {
                    Console.WriteLine("Current node: " + current.name);
                }
            }
            else
            {
                Console.WriteLine("Beginning of List");
            }
        }

        public void addNodeAfter(String name)
        {
            if(current == tail) {
                current = current.prev;
            }
            Node newNode = new Node();
            newNode.name = name;

            newNode.next = current.next;
            newNode.prev = current;
            current.next.prev = newNode;
            current.next = newNode;
            current = current.next;

            Console.WriteLine("Added node: " + current.name);
        }

        public void addNodeBefore(String name)
        {
            if (current == head)
            {
                current = current.next;
            }
            Node newNode = new Node();
            newNode.name = name;

            newNode.next = current;
            newNode.prev = current.prev;
            current.prev.next = newNode;
            current.prev = newNode;
            current = current.prev;

            Console.WriteLine("Added node: " + current.name);
        }

        public void deleteCurrent()
        {
            if(current != head && current != tail)
            {
                current.prev.next = current.next; //Connect the previous node to the next node
                current.next.prev = current.prev; //Connect the next node to the previous node
                Console.WriteLine(current.name + " has been removed.");
                if (current.prev != head)
                {
                    current = current.prev;
                }
                else
                {
                    current = current.next;
                }
            }
            else
            {
                Console.WriteLine("List is already empty.");
            }
        }

        public void deleteAll()
        {
            head.next = tail;
            tail.prev = head;
            current = head;
        }

        public void printNodeNames()
        {
            Node reader = head; //Using a new pointer here means that the user's spot is not changed during the reading process.

            if (reader.next == tail)
            {
                Console.WriteLine("Empty List.");
            }
            else
            {
                while(reader.next != null && reader.next != tail)
                {
                    reader = reader.next;
                    Console.WriteLine(reader.name);
                }
            }
        }

        public void updateNode(String newName)
        {
            if(current.name != null)
            {
                Console.WriteLine("Confirm change of " + current.name + " to " + newName + " (Y/N)");
                String updateConfirm = Console.ReadLine();
                if (updateConfirm.StartsWith("Y"))
                {
                    current.name = newName;
                    Console.WriteLine("Updated.");
                }
                else
                {
                    Console.WriteLine("Confirmation not accepted. List retained.");
                }
            }
            else
            {
                Console.WriteLine("Unable to update. Valid node not targeted.");
            }
        }
    }
}

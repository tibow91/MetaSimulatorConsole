using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaSimulatorConsole
{
    public class MyLinkedList<T> 
    {
	
	    private T value;
	    private MyLinkedList<T> next = null;
	    private MyLinkedList<T> previous = null;
	
	    public T getValue()	{
		    return value;
	    }
	
	    public void setValue(T value){
		    this.value = value;
	    }
	
	    public MyLinkedList<T> getNext(){
		    return next;
	    }
	
	    public void setNext(MyLinkedList<T> next){
		    this.next = next;
		    if(getNext() != null && getNext().getPrevious() != this)
			    getNext().setPrevious(this);
	    }
	
	    public MyLinkedList<T> getPrevious() {
		    return previous;
	    }

	    public void setPrevious(MyLinkedList<T> previous) {
		    this.previous = previous;
		    if(getPrevious() != null && getPrevious().getNext() != this)
			    getPrevious().setNext(this);
	    }
	
	    public void remove(){
		
		    if(getPrevious() != null)
			    getPrevious().setNext(getNext());
		
		    if(getNext() != null)
			    getNext().setPrevious(getPrevious());	
		    setNext(null);
		    setPrevious(null);
	    }
	
	    public void setNewEndLinkedList(T val){
		
		    if(getNext() == null)
			    setNext(new MyLinkedList<T>(val));
		
		    else
			    getNext().setNewEndLinkedList(val);
	    }

        public MyLinkedList()
        {
	    }
	    public MyLinkedList (T val){
		    setValue(val);
	    }
	
    }

}

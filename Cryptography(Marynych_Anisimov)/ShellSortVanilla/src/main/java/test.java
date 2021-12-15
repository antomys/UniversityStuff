import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;

class HeapNode {

    int element;
    int arrayIndex;
    int nextElementIndex = 1;

    public HeapNode(int element, int arrayIndex) {
        this.element = element;
        this.arrayIndex = arrayIndex;
    }
}

class MinHeap {

    HeapNode[] heapNodes;

    public MinHeap(HeapNode heapNodes[]) {
        this.heapNodes = heapNodes;
        heapifyFromLastLeafsParent();
    }

    void heapifyFromLastLeafsParent() {
        int lastLeafsParentIndex = getParentNodeIndex(heapNodes.length);
        while (lastLeafsParentIndex >= 0) {
            heapify(lastLeafsParentIndex);
            lastLeafsParentIndex--;
        }
    }

    void heapify(int index) {
        int leftNodeIndex = getLeftNodeIndex(index);
        int rightNodeIndex = getRightNodeIndex(index);
        int smallestElementIndex = index;
        if (leftNodeIndex < heapNodes.length && heapNodes[leftNodeIndex].element < heapNodes[index].element) {
            smallestElementIndex = leftNodeIndex;
        }
        if (rightNodeIndex < heapNodes.length && heapNodes[rightNodeIndex].element < heapNodes[smallestElementIndex].element) {
            smallestElementIndex = rightNodeIndex;
        }
        if (smallestElementIndex != index) {
            swap(index, smallestElementIndex);
            heapify(smallestElementIndex);
        }
    }

    int getParentNodeIndex(int index) {
        return (index - 1) / 2;
    }

    int getLeftNodeIndex(int index) {
        return (2 * index + 1);
    }

    int getRightNodeIndex(int index) {
        return (2 * index + 2);
    }

    HeapNode getRootNode() {
        return heapNodes[0];
    }

    void heapifyFromRoot() {
        heapify(0);
    }

    void swap(int i, int j) {
        HeapNode temp = heapNodes[i];
        heapNodes[i] = heapNodes[j];
        heapNodes[j] = temp;
    }

    static void  merge(ArrayList<ArrayList<Integer>> array) {
        HeapNode[] heapNodes = new HeapNode[array.size()];
        int resultingArraySize = 0;

        for (int i = 0; i < array.size(); i++) {
            HeapNode node = new HeapNode(array.get(i).get(0), i);
            heapNodes[i] = node;
            resultingArraySize += array.get(i).size();
        }

        MinHeap minHeap = new MinHeap(heapNodes);
        try {
            FileWriter fileWriter = new FileWriter("src/main/resources/output.txt",false);
            for (int i = 0; i < resultingArraySize; i++) {
                HeapNode root = minHeap.getRootNode();
                int rootElement = root.element;
                fileWriter.write(String.valueOf(rootElement) +'\n');


                if (root.nextElementIndex < array.get(root.arrayIndex).size()) {
                    root.element = array.get(root.arrayIndex).get(root.nextElementIndex++);
                } else {
                    root.element = Integer.MAX_VALUE;
                }
                minHeap.heapifyFromRoot();
            }
            fileWriter.close();
        }
        catch (IOException e) {
            e.printStackTrace();
        }
    }
}
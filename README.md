# ARCase
AR Project <br/>
**Purpose** <br/>
The purpose of the project is to allow users to create an immersive world for themselves using the objects in their inventory.

**How does it work?** <br/>
 Click on the build file<br/>
 
**Technologies**<br/>
Developed using AR Foundation and Unity.<br/>

**Create New Object** <br/>

![Screenshot 2023-08-28 204258](https://github.com/ayseatabey/ARCase/assets/32060251/3709d8ad-81d4-4ce7-81bb-eb350a5c4616)  <br/>
Simple implementation of the DraggableObject method.

**Change Draggable Object Type** <br/>
![Screenshot 2023-08-28 205346](https://github.com/ayseatabey/ARCase/assets/32060251/a3eb5ddf-1149-4cc7-80d2-eafccfd02db9)  <br/>

**Plane Detection** <br/>

![Screenshot 2023-08-28 205816](https://github.com/ayseatabey/ARCase/assets/32060251/97abd398-99c6-439b-a8e7-26910c5c70e6) <br/>

Plane detection implementation.<br/>

**Summary**<br/>
When you launch the application, the camera opens and  a background sound is heard. <br/>
There are two types of objects. Objects can be dragged and added to the AR world. <br/>
One type of object can be freely dragged and placed anywhere. The other type spawns when vertical or horizontal surfaces are detected. <br/>
You can also replace these objects by selecting a different type from the inventory. <br/>
While dragging an object, it has an outline effect around it which disappears when you release it.<br/>
If you successfully place the objects, they emit a unique sound associated with each object.<br/>
If no suitable plane is detected where you release the object, it returns to the inventory.<br/>

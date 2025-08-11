Unity内置管线（Built-in Pipeline）和通用渲染管线（Universal Render Pipeline，简称URP）

渲染管线概述 Unity内置管线是Unity3D默认的渲染管线，提供了一套完整的渲染功能，包括渲染对象的光照、阴影、材质和特效等。它使用了基于Forward Rendering（前向渲染）的方式进行渲染，适用于大多数游戏项目。

URP管线是Unity3D推出的通用渲染管线，旨在提供更高效、更灵活的渲染解决方案。URP基于SRP（Scriptable Render Pipeline，可编程渲染管线）技术，允许开发者自定义渲染流程，并支持多平台、多设备的渲染。URP还提供了一些额外的功能和效果，比如体积光、后处理效果等。

**渲染功能比较** Unity内置管线和URP管线在渲染功能上有一些区别。首先，Unity内置管线支持更多的渲染特性，比如实时阴影、全局光照等。而URP管线在某些方面可能会有一些限制，例如在移动设备上不支持实时阴影。

其次，URP管线引入了一种新的渲染方式，即基于Deferred Rendering（延迟渲染）的渲染路径。这种方式可以提高渲染效率，尤其在处理大量光源和复杂材质时更为明显。而Unity内置管线使用的是Forward Rendering，对于复杂场景和大量光源的情况下性能可能会受到限制。

**可编程性比较** URP管线相比于Unity内置管线更加可编程。URP使用C#脚本来定义渲染流程，开发者可以通过自定义脚本来修改渲染过程中的各个阶段。这意味着开发者可以更加灵活地控制渲染流程，实现一些特定的需求。而Unity内置管线的渲染流程是固定的，无法进行定制。

**性能比较** URP管线相对于Unity内置管线在性能上有一些优势。URP使用了一些优化技术，比如使用了SRP Batcher来减少Draw Call的数量，使用了GPU Instancing来提高渲染效率等。这些优化技术可以有效地提高渲染性能，特别是在处理大量可见对象时。而Unity内置管线在性能方面可能会受到一些限制，特别是在处理复杂的渲染场景时。

---

Time.deltatime: different machine, different deltatime
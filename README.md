# Project Overview for WfP2
This project is designed to create a 3D world where various performance-enhancing techniques will be evaluated, specifically Occlusion Culling, Object Pooling, and Level of Detail. 

The project is developed using Unity (Editor Version: 2022.3.49f1)

It will serve as the basis for BA2, where the measurement of performance indicators will be discussed in detail.

In the following, each requirement specified for WfP2 is listed.

## MUST
M1 ✅ Es muss einen fixen Punkt geben, an dem die Performancemessung durchgeführt wird (zwecks Fairness) (Messung im Stillstand)

M2 ✅ An diesen Punkten muss die Kamera automatisch ausgerichtet werden, sodass immer in dieselbe Richtung mit derselben Höhe geschaut wird.

M3 ✅ Die Performance muss durch diese Punkte reproduzierbar mit denselben Grundbedingungen gemessen werden können. (Sprich bei Messung mit Technik und Messung ohne Technik gibt es keine Unterschiede bezüglich Kameraposition, Sichtrichtung, etc. sodass die Performance fair gemessen werden kann)

M4 ✅ Die Performance muss über den Unity Profiler messbar sein.

M5 ✅ Das Spiel muss Occlusion Culling beinhalten.

M6 ✅ Das Spiel muss Objekte beinhalten die Level-of-Detail implementieren.

M7 ✅ Es muss einen Punkt geben an dem man Object Pooling testen kann (z.B. Wasserfall, mit einem GameObject pro Wassertextur/Wasserpartikel)

## SHOULD
S1 ✅ Das Spiel sollte bewusst Objekte verstecken, um die Wirkung von Occlusion Culling zu verdeutlichen (z.B. viele Bäume hinter großen Stein oder ein kleines Haus mit sehr viel kleinen Inneneinrichtungen)

S2 ✅ Es sollte noch weitere Punkte für die Performancemessung geben, an denen dann auch die Kamera automatisch eingestellt wird (Messung aus mehreren Blickwinkeln).

## COULD
C1 ✅ Das Spiel könnte auch selbst modellierte Objekte beinhalten, falls bestimmte Texturen gewünscht sind aber nicht durch Assets verfügbar sind.

C2 ✅ Das Spiel könnte eine Route/Pfad durch die Welt haben, der automatisch entlanggegangen wird und auch zur Performancemessung herangezogen wird (Messung in Bewegung).

### 3D-modellierte Texturen
* WasserFall Object
* Grotte
* Schild mit Beschriftung
* Druckplatte
* Wegpfeiler

### Used Assets
* https://assetstore.unity.com/packages/3d/environments/stylized-fantasy-house-153587
* https://assetstore.unity.com/packages/3d/environments/fantasy/lowpoly-baker-s-house-26443
* https://assetstore.unity.com/packages/3d/environments/landscapes/low-poly-simple-nature-pack-162153
* https://assetstore.unity.com/packages/3d/environments/landscapes/terrain-sample-asset-pack-145808

